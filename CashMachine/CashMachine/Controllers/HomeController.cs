using CashMachine.Common;
using CashMachine.DAL.Models;
using CashMachine.DAL.Repository;
using CashMachine.Extensions;
using CashMachine.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CashMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            Session.SetDataToSession<bool>(Constants.IsAuthorizedKey, false);
            Session.SetDataToSession<string>(Constants.CardNumberKey, null);
            return View();
        }

        public ActionResult CheckCardNum(string inputCardNum)
        {
            if (string.IsNullOrEmpty(inputCardNum))
            {
                TempData[Constants.ErrorTextKey] = Resources.CardNotFound;
                return RedirectToAction("Error");
            }

            var cardNumber = inputCardNum.Replace(Constants.Dash, string.Empty);
            Card card = _repository.GetCardById(cardNumber);
            if (card == null)
            {
                TempData[Constants.ErrorTextKey] = Resources.CardNotFound;
                return RedirectToAction("Error");
            }

            Session.SetDataToSession<string>(Constants.CardNumberKey, cardNumber);
            return RedirectToAction("PinCode");
        }

        public ActionResult PinCode()
        {
            Session.SetDataToSession<bool>(Constants.IsAuthorizedKey, false);
            if (Session.GetDataFromSession<string>(Constants.CardNumberKey) == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult CheckPinCode(string inputPinCode)
        {
            var id = Session.GetDataFromSession<string>(Constants.CardNumberKey);
            var pinCode = inputPinCode;
            var card = _repository.GetCardByIdAndPinCode(id, pinCode);
            if (card != null)
            {
                NullifyTriesCount(id);
                Session.SetDataToSession<bool>(Constants.IsAuthorizedKey, true);
                return RedirectToAction("Operation");
            }

            TempData[Constants.ErrorTextKey] =
                CheckTriesCountIsValid(id) ? Resources.InvalidPinCode : Resources.CardIsBlocked;
            return RedirectToAction("Error");
        }

        [Authorized]
        public ActionResult Operation()
        {
            return View();
        }

        [Authorized]
        public ActionResult Balance()
        {
            var action = _repository.GetActionByDescription(Constants.ViewBalanceDescription);
            var card = _repository.GetCardById(Session.GetDataFromSession<string>(Constants.CardNumberKey));

            if (action == null || card == null)
            {
                TempData[Constants.ErrorTextKey] = Resources.ErrorShowingBalance;
                return RedirectToAction("Error");
            }

            var operation = _repository.AddOperation(card.Id, action.Id);

            return View(_repository.GetOperationIncludeCardById(operation.Id));
        }

        [Authorized]
        public ActionResult Cash()
        {
            return View();
        }

        [Authorized]
        public ActionResult OperationResult(string inputSum = Constants.Empty)
        {
            inputSum = inputSum.TrimStart('0');
            int.TryParse(inputSum, out var sum);
            if (!string.IsNullOrEmpty(inputSum) && sum == 0)
            {
                TempData[Constants.ErrorTextKey] = Resources.SumExceedsLimit;
                return RedirectToAction("Error");
            }

            var action = _repository.GetActionByDescription(Constants.GetCashDescription);
            var card = _repository.GetCardById(Session.GetDataFromSession<string>(Constants.CardNumberKey));

            if (action == null || card == null)
            {
                TempData[Constants.ErrorTextKey] = Resources.ErrorGettingCash;
                return RedirectToAction("Error");
            }

            if (sum < 0)
            {
                sum = 0;
            }

            card.Sum -= sum;

            if (card.Sum < 0)
            {
                TempData[Constants.ErrorTextKey] = Resources.NotEnoughMoney;
                return RedirectToAction("Error");
            }

            var operation = _repository.AddOperation(card.Id, action.Id, sum);
            _repository.EditCard(card);

            return View(_repository.GetOperationIncludeCardById(operation.Id));
        }

        public ActionResult Error()
        {
            if (TempData[Constants.ErrorTextKey] == null)
            {
                TempData[Constants.ErrorTextKey] = Resources.DefaultErrorMessage;
            }
            return View();
        }

        private bool CheckTriesCountIsValid(string id)
        {
            var invalidPinCodes = Session.GetDataFromSession<Dictionary<string, int>>(Constants.InvalidPinCodesKey);
            if (invalidPinCodes == null)
            {
                Session.SetDataToSession<Dictionary<string, int>>(Constants.InvalidPinCodesKey, new Dictionary<string, int>());
                invalidPinCodes = Session.GetDataFromSession<Dictionary<string, int>>(Constants.InvalidPinCodesKey);
            }
            if (!invalidPinCodes.ContainsKey(id))
            {
                invalidPinCodes.Add(id, 1);
            }
            else
            {
                if (invalidPinCodes[id] == Constants.ValidPinCodeTriesCount)
                {
                    _repository.BlockCard(id);
                }
                invalidPinCodes[id]++;
            }
            return invalidPinCodes[id] <= Constants.ValidPinCodeTriesCount;
        }

        private void NullifyTriesCount(string id)
        {
            var invalidPinCodes = Session.GetDataFromSession<Dictionary<string, int>>(Constants.InvalidPinCodesKey);
            if (invalidPinCodes != null && invalidPinCodes.ContainsKey(id))
            {
                invalidPinCodes[id] = 0;
            }
        }
    }
}