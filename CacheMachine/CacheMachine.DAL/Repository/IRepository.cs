using CacheMachine.DAL.Models;

namespace CacheMachine.DAL.Repository
{
    public interface IRepository
    {
        Operation AddOperation(string cardId, int actionId, int? sum = null);

        Card BlockCard(string cardNum);

        Card EditCard(Card card);

        Action GetActionByDescription(string description);

        Card GetCardById(string cardNum);

        Card GetCardByIdAndPinCode(string cardNum, string pinCode);

        Operation GetOperationIncludeCardById(int id);
    }
}