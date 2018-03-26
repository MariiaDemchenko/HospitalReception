﻿using CacheMachine.Common;
using CacheMachine.DAL.Models;
using CacheMachine.DAL.Repository;
using CacheMachine.Extensions;
using CacheMachine.Filters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class HomeController : Controller
{
    private readonly IRepository _repository;

    public HomeController(IRepository repository)
    {
        _repository = repository;
    }

    public ActionResult Index()
    {
        Session.SetDataToSession<bool>("IsAuthorized", false);
        Session.SetDataToSession<string>("CardNumber", null);
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

        Session.SetDataToSession<string>("CardNumber", cardNumber);
        return RedirectToAction("PinCode");
    }

    [Authorized]
    public ActionResult Cache(string id)
    {
        return View(id as object);
    }

    public ActionResult CheckPinCode(string inputField)
    {
        var id = Session.GetDataFromSession<string>("CardNumber");
        var pinCode = inputField;
        var card = _repository.GetCardByIdAndPinCode(id, pinCode);
        if (card != null)
        {
            NullifyTriesCount(id);
            Session.SetDataToSession<bool>("IsAuthorized", true);
            return RedirectToAction("Operation");
        }

        var message = CheckTriesCountIsValid(id) ? Resources.InvalidPinCode : Resources.CardIsBlocked;

        return RedirectToAction("Error", new { message });
    }

    public ActionResult PinCode()
    {
        Session.SetDataToSession<bool>("IsAuthorized", false);
        if (Session.GetDataFromSession<string>("CardNumber") == null)
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
        return View(Session.GetDataFromSession<object>("CardNumber"));
    }

    [Authorized]
    public ActionResult Balance()
    {
        var action = _repository.GetActionByDescription(Resources.ViewBalance);
        var card = _repository.GetCardById(Session.GetDataFromSession<string>("CardNumber"));

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
        var card = _repository.GetCardById(Session.GetDataFromSession<string>("CardNumber"));

        if (action == null || card == null)
        {
            return RedirectToAction("Error", new { message = Resources.ErrorGettingCache });
        }

        if (sum < 0)
        {
            sum = 0;
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
        var invalidPinCodes = Session.GetDataFromSession<Dictionary<string, int>>("InvalidPinCodes");
        if (invalidPinCodes == null)
        {
            Session.SetDataToSession<Dictionary<string, int>>("InvalidPinCodes", new Dictionary<string, int>());
            invalidPinCodes = Session.GetDataFromSession<Dictionary<string, int>>("InvalidPinCodes");
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
        var invalidPinCodes = Session.GetDataFromSession<Dictionary<string, int>>("InvalidPinCodes");
        if (invalidPinCodes != null && invalidPinCodes.ContainsKey(id))
        {
            invalidPinCodes[id] = 0;
        }
    }
}