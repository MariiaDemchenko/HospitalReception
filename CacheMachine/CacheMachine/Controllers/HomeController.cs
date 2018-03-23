using CacheMachine.Common;
using CacheMachine.DAL.Models;
using CacheMachine.DAL.Repository;
using CacheMachine.Filters;
using CacheMachine.Helpers;
using System;
using System.Web.Mvc;

public class HomeController : Controller
{
    private readonly IRepository _repository;
    private readonly SessionHelper _session;

    public HomeController()
    {
        _repository = new CacheMachineRepository();
        _session = new SessionHelper();
    }

    public ActionResult Index()
    {
        _session.IsAuthorized = false;
        _session.CardNumber = null;
        return View();
    }

    public ActionResult CheckCardNum(string inputField)
    {
        if (string.IsNullOrEmpty(inputField))
        {
            return RedirectToAction("Error", new { message = Resources.CardNotFound });
        }

        var cardNumber = inputField.Replace(Constants.Dash, string.Empty);
        Card card = _repository.GetCardById(cardNumber);
        if (card == null)
        {
            return RedirectToAction("Error", new { message = Resources.CardNotFound });
        }

        _session.CardNumber = cardNumber;
        return RedirectToAction("PinCode");
    }

    [Authorized]
    public ActionResult Cache(string id)
    {
        return View(id as object);
    }

    public ActionResult CheckPinCode(string inputField)
    {
        var id = _session.CardNumber;
        var pinCode = inputField;
        var card = _repository.GetCardByIdAndPinCode(id, pinCode);
        if (card != null)
        {
            NullifyTriesCount(id);
            _session.IsAuthorized = true;
            return RedirectToAction("Operation");
        }

        var message = CheckTriesCountIsValid(id) ? Resources.InvalidPinCode : Resources.CardIsBlocked;

        return RedirectToAction("Error", new { message });
    }

    public ActionResult PinCode()
    {
        _session.IsAuthorized = false;
        if (_session.CardNumber == null)
        {
            return RedirectToAction("Index");
        }
        return View();
    }

    public ActionResult Error(string message)
    {
        ViewBag.Message = message;
        return View();
    }

    [Authorized]
    public ActionResult Operation()
    {
        return View(_session.CardNumber as object);
    }

    [Authorized]
    public ActionResult Balance()
    {
        var action = _repository.GetActionByDescription(Resources.ViewBalance);
        var card = _repository.GetCardById(_session.CardNumber);

        if (action == null || card == null)
        {
            return RedirectToAction("Error", new { message = Resources.ErrorShowingBalance });
        }

        var operation = new Operation
        {
            OperationDate = DateTime.Now,
            CardId = card.Id,
            ActionId = action.Id
        };

        _repository.AddOperation(operation);

        return View(_repository.GetOperationIncludeCardById(operation.Id));
    }

    [Authorized]
    public ActionResult OperationResult(string inputField = Constants.Empty)
    {
        int.TryParse(inputField, out var sum);
        var action = _repository.GetActionByDescription(Resources.GetCache);
        var card = _repository.GetCardById(_session.CardNumber);

        if (action == null || card == null)
        {
            return RedirectToAction("Error", new { message = Resources.ErrorGettingCache });
        }

        card.Sum -= sum;

        if (card.Sum < 0)
        {
            return RedirectToAction("Error", new { message = Resources.NotEnoughMoney });
        }

        var operation = new Operation
        {
            OperationDate = DateTime.Now,
            Sum = sum,
            CardId = card.Id,
            ActionId = action.Id
        };

        _repository.AddOperation(operation);
        _repository.EditCard(card);

        return View(_repository.GetOperationIncludeCardById(operation.Id));
    }

    private bool CheckTriesCountIsValid(string id)
    {
        var invalidPinCodes = _session.InvalidPinCodes;
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
        var invalidPinCodes = _session.InvalidPinCodes;
        if (invalidPinCodes.ContainsKey(id))
        {
            invalidPinCodes[id] = 0;
        }
    }
}