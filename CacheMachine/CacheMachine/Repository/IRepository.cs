using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheMachine.Models;

namespace CacheMachine.Repository
{
    public interface IRepository
    {
        List<Card> GetAllCards();

        List<Operation> GetAllOperations();

        List<Models.Action> GetAllActions();

        Card GetCardById(long cardNum);

        Operation AddOperation(Operation operation);

        Card EditCard(Card card);

        Card GetCardByIdAndPinCode(long cardNum, int pinCode);

        Models.Action GetActionByDescription(string description);

        Operation GetOperationIncludeCardById(int id);

        Card BlockCard(long cardNum);
    }
}
