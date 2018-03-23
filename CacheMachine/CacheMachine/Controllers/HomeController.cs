using CacheMachine.Common;
using CacheMachine.DAL.Models;
using CacheMachine.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class HomeController : Controller
{
    private readonly IRepository _repository;

    public HomeController()
    {
        _repository = new CacheMachineRepository();
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult CheckCardNum(string inputField)
    {
        var cardNum = inputField;
        Card card = null;
        if (!string.IsNullOrEmpty(cardNum))
        {
            var cardNumber = cardNum.Replace(Constants.Dash, string.Empty);
            card = _repository.GetCardById(cardNumber);
        }
        return card != null ? RedirectToAction("PinCode", new { id = card.Id }) : RedirectToAction("Error", new { message = Resources.CardNotFound });
    }

    public ActionResult Cache(string id)
    {
        return View(id as object);
    }

    public ActionResult CheckPinCode(string id, string inputField)
    {
        var pinCode = inputField;
        var card = _repository.GetCardByIdAndPinCode(id, pinCode);
        if (card != null)
        {
            NullifyTriesCount(id);
            return RedirectToAction("Operation", new { id = card.Id });
        }

        var message = CheckTriesCountIsValid(id) ? Resources.InvalidPinCode : Resources.CardIsBlocked;

        return RedirectToAction("Error", new { message });
    }

    public ActionResult PinCode(string id)
    {
        return View(new Card { Id = id });
    }

    public ActionResult Error(string message)
    {
        ViewBag.Message = message;
        return View();
    }

    public ActionResult Operation(string id)
    {
        return View(id as object);
    }

    public ActionResult Balance(string id)
    {
        var action = _repository.GetActionByDescription(Resources.ViewBalance);
        var card = _repository.GetCardById(id);

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

    public ActionResult OperationResult(string id, string inputField = Constants.Empty)
    {
        int.TryParse(inputField, out var sum);
        var action = _repository.GetActionByDescription(Resources.GetCache);
        var card = _repository.GetCardById(id);

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
        var invalidPinCodes = GetInvalidPinCodes();
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
        var invalidPinCodes = GetInvalidPinCodes();
        if (invalidPinCodes.ContainsKey(id))
        {
            invalidPinCodes[id] = 0;
        }
    }

    private Dictionary<string, int> GetInvalidPinCodes()
    {
        return System.Web.HttpContext.Current.Session["InvalidPinCodes"] as Dictionary<string, int>;
    }
}