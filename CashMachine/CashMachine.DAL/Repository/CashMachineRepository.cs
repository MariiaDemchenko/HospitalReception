using CashMachine.Common;
using CashMachine.DAL.Models;
using System;
using System.Data.Entity;
using System.Linq;
using Action = CashMachine.DAL.Models.Action;

namespace CashMachine.DAL.Repository
{
    public class CashMachineRepository : IRepository
    {
        private readonly HashcodeHelper _hashHelper;

        public CashMachineRepository()
        {
            _hashHelper = new HashcodeHelper();
        }

        public Operation AddOperation(string cardId, int actionId, int? sum = null)
        {
            var operation = new Operation
            {
                OperationDate = DateTime.Now,
                CardId = cardId,
                ActionId = actionId,
                Sum = sum
            };

            using (var db = new CashMachineContext())
            {
                db.Operations.Add(operation);
                db.SaveChanges();
            }

            return operation;
        }

        public Card BlockCard(string cardNum)
        {
            var card = GetCardById(cardNum);
            card.IsBlocked = true;
            using (var db = new CashMachineContext())
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
            }
            return card;
        }

        public Card EditCard(Card card)
        {
            using (var db = new CashMachineContext())
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
            }
            return card;
        }

        public Action GetActionByDescription(string description)
        {
            Action action;
            using (var context = new CashMachineContext())
            {
                action = context.Actions.FirstOrDefault(a => a.Description == description);
            }

            return action;
        }

        public Card GetCardById(string cardNum)
        {
            Card card;
            using (var context = new CashMachineContext())
            {
                card = context.Cards.FirstOrDefault(c => c.Id == cardNum && !c.IsBlocked);
            }
            return card;
        }

        public Card GetCardByIdAndPinCode(string cardNum, string pinCode)
        {
            Card card;
            bool match;
            using (var context = new CashMachineContext())
            {
                card = context.Cards.FirstOrDefault(c => c.Id == cardNum && !c.IsBlocked);
                match = _hashHelper.CheckHashMatch(pinCode, card?.PinCodeSalt, card?.PinCodeHash);
            }
            return match ? card : null;
        }

        public Operation GetOperationIncludeCardById(int id)
        {
            Operation operation;
            using (var context = new CashMachineContext())
            {
                operation = context.Operations.Include(o => o.Card).FirstOrDefault(oper => oper.Id == id);
            }
            return operation;
        }
    }
}