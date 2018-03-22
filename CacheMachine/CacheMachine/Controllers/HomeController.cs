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

    public ActionResult CheckCardNum(string inputField)
    {
        var cardNum = inputField;
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
        return View(id);
    }

    public ActionResult CheckPinCode(long id, string inputField)
    {
        int.TryParse(inputField, out var pinCode);
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
            return RedirectToAction("Error", new { message = "Произошла ошибка при отображении данных о балансе" });
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
            return RedirectToAction("Error", new { message = "Произошла ошибка при попытке снятия денег" });
        }

        card.Sum -= sum;

        if (card.Sum < 0)
        {
            return RedirectToAction("Error", new { message = "Недостаточно средств" });
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
}