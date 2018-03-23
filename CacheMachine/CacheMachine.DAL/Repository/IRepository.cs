using CacheMachine.DAL.Models;
using System.Collections.Generic;

namespace CacheMachine.DAL.Repository
{
    public interface IRepository
    {
        List<Card> GetAllCards();

        List<Operation> GetAllOperations();

        List<Action> GetAllActions();

        Card GetCardById(string cardNum);

        Operation AddOperation(Operation operation);

        Card EditCard(Card card);

        Card GetCardByIdAndPinCode(string cardNum, string pinCode);

        Action GetActionByDescription(string description);

        Operation GetOperationIncludeCardById(int id);

        Card BlockCard(string cardNum);
    }
}