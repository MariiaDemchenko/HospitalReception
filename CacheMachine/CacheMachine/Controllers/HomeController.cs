using CacheMachine.Models;
using CacheMachine.Repository;
using System;
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

    public ActionResult CheckCardNum(string cardNum)
    {
        Card card = null;
        if (!string.IsNullOrEmpty(cardNum))
        {
            var cardNumber = long.Parse(cardNum.Replace("-", string.Empty));
            card = _repository.GetCardById(cardNumber);
        }
        return card != null ? RedirectToAction("PinCode", new { id = card.Id }) : RedirectToAction("Error", new { message = "Карта не найдена" });
    }

    public ActionResult Cache(long id)
    {
        return PartialView(id);
    }

    public ActionResult CheckPinCode(long id, int pinCode)
    {
        var card = _repository.GetCardByIdAndPinCode(id, pinCode);
        if (card != null)
        {
            return RedirectToAction("Operation", new { id = card.Id });
        }

        return RedirectToAction("Error", new { message = "Неверный пин-код" });
    }

    public ActionResult PinCode(long id)
    {
        //TODO: проверка 4-кратного ввода ошибочного пароля
        return PartialView(new Card { Id = id });
    }

    public ActionResult Error(string message = "")
    {
        //TODO: кнопка назад + обработать все ситуации ошибок
        ViewBag.Message = message;
        return PartialView();
    }

    public ActionResult Operation(long id)
    {
        return PartialView(id);
    }

    public ActionResult Balance(long id)
    {
        var action = _repository.GetActionByDescription("Просмотр баланса");
        var card = _repository.GetCardById(id);

        if (action == null || card == null)
        {
            return RedirectToAction("Error");
        }

        var operation = new Operation
        {
            OperationDate = DateTime.Now,
            CardId = card.Id,
            OptionId = action.Id
        };

        _repository.AddOperation(operation);

        return PartialView(_repository.GetOperationIncludeCardById(operation.Id));
    }

    public ActionResult OperationResult(long id, int sum = 0)
    {
        var action = _repository.GetActionByDescription("Снятие денег");
        var card = _repository.GetCardById(id);

        if (action == null || card == null)
        {
            return RedirectToAction("Error");
        }

        card.Sum -= sum;

        if (card.Sum < 0)
        {
            return RedirectToAction("Error");
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

        return PartialView(_repository.GetOperationIncludeCardById(operation.Id));
    }
}