using CacheMachine.Models;
using CacheMachine.Properties;
using CacheMachine.Repository;
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
            var cardNumber = long.Parse(cardNum.Replace("-", string.Empty));
            card = _repository.GetCardById(cardNumber);
        }
        return card != null ? RedirectToAction("PinCode", new { id = card.Id }) : RedirectToAction("Error", new { message = Resources.CardNotFound });
    }

    public ActionResult Cache(long id)
    {
        return View(id);
    }

    public ActionResult CheckPinCode(long id, string inputField)
    {
        int.TryParse(inputField, out var pinCode);
        var card = _repository.GetCardByIdAndPinCode(id, pinCode);
        if (card != null)
        {
            NullifyTriesCount(id);
            return RedirectToAction("Operation", new { id = card.Id });
        }

        var message = CheckTriesCountIsValid(id) ? Resources.InvalidPinCode : Resources.CardIsBlocked;

        return RedirectToAction("Error", new { message });
    }

    public ActionResult PinCode(long id)
    {
        return View(new Card { Id = id });
    }

    public ActionResult Error(string message = "")
    {
        ViewBag.Message = message;
        return View();
    }

    public ActionResult Operation(long id)
    {
        return View(id);
    }

    public ActionResult Balance(long id)
    {
        var action = _repository.GetActionByDescription("Просмотр баланса");
        var card = _repository.GetCardById(id);

        if (action == null || card == null)
        {
            return RedirectToAction("Error", new { message = Resources.ErrorShowingBalance });
        }

        var operation = new Operation
        {
            OperationDate = DateTime.Now,
            CardId = card.Id,
            OptionId = action.Id
        };

        _repository.AddOperation(operation);

        return View(_repository.GetOperationIncludeCardById(operation.Id));
    }

    public ActionResult OperationResult(long id, string inputField = "")
    {
        var sum = int.Parse(inputField);
        var action = _repository.GetActionByDescription("Снятие денег");
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
            OptionId = action.Id
        };

        _repository.AddOperation(operation);
        _repository.EditCard(card);

        return View(_repository.GetOperationIncludeCardById(operation.Id));
    }

    private bool CheckTriesCountIsValid(long id)
    {
        var invalidPinCodes = GetInvalidPinCodes();
        if (!invalidPinCodes.ContainsKey(id))
        {
            invalidPinCodes.Add(id, 1);
        }
        else
        {
            if (invalidPinCodes[id] == 3)
            {
                _repository.BlockCard(id);
            }
            invalidPinCodes[id]++;
        }
        return invalidPinCodes[id] <= 3;
    }

    private void NullifyTriesCount(long id)
    {
        var invalidPinCodes = GetInvalidPinCodes();
        if (invalidPinCodes.ContainsKey(id))
        {
            invalidPinCodes[id] = 0;
        }
    }

    private Dictionary<long, int> GetInvalidPinCodes()
    {
        return System.Web.HttpContext.Current.Session["InvalidPinCodes"] as Dictionary<long, int>;
    }
}