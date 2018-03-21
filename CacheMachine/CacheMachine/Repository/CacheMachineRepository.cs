using CacheMachine.DataAccessLayer;
using CacheMachine.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CacheMachine.Repository
{
    //TODO: оптимизировать/убрать ненужные методы
    public class CacheMachineRepository : IRepository
    {
        public List<Card> GetAllCards()
        {
            List<Card> users;
            using (var context = new CacheMachineContext())
            {
                users = context.Cards.ToList();
            }

            return users;
        }

        public List<Operation> GetAllOperations()
        {
            List<Operation> operations;
            using (var context = new CacheMachineContext())
            {
                operations = context.Operations.ToList();
            }

            return operations;
        }

        public Card GetCardById(long cardNum)
        {
            Card card;
            using (var context = new CacheMachineContext())
            {
                card = context.Cards.FirstOrDefault(c => c.Id == cardNum && !c.IsBlocked);
            }
            return card;
        }

        public Card GetCardByIdAndPinCode(long cardNum, int pinCode)
        {
            Card card;
            using (var context = new CacheMachineContext())
            {
                card = context.Cards.FirstOrDefault(c => c.Id == cardNum && c.PinCode == pinCode && !c.IsBlocked);
            }
            return card;
        }

        public List<Action> GetAllActions()
        {
            List<Action> actions;
            using (var context = new CacheMachineContext())
            {
                actions = context.Actions.ToList();
            }

            return actions;
        }

        public Action GetActionByDescription(string description)
        {
            Action action;
            using (var context = new CacheMachineContext())
            {
                action = context.Actions.FirstOrDefault(a => a.Description == description);
            }

            return action;
        }

        public Operation AddOperation(Operation operation)
        {
            using (var db = new CacheMachineContext())
            {
                db.Operations.Add(operation);
                db.SaveChanges();
            }

            return operation;
        }

        public Operation GetOperationIncludeCardById(int id)
        {
            Operation operation;
            using (var context = new CacheMachineContext())
            {
                operation = context.Operations.Include(o => o.Card).FirstOrDefault(oper => oper.Id == id);
            }
            return operation;
        }

        public Card EditCard(Card card)
        {
            using (var db = new CacheMachineContext())
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
            }
            return card;
        }
    }
}