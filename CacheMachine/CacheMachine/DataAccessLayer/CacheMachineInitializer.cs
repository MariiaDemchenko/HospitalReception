using CacheMachine.Models;
using System.Collections.Generic;

namespace CacheMachine.DataAccessLayer
{
    public class CacheMachineInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CacheMachineContext>
    {
        protected override void Seed(CacheMachineContext context)
        {
            var cards = new List<Card>
            {
            new Card{Id = 1111222233334444, IsBlocked = false, PinCode = 1234, Sum = 1000},
            new Card{Id = 2222333344445555, IsBlocked = false, PinCode = 2345, Sum = 2000},
            new Card{Id = 3333444455556666, IsBlocked = false, PinCode = 3456, Sum = 3000},
            new Card{Id = 4444555566667777, IsBlocked = false, PinCode = 4567, Sum = 4000},
            new Card{Id = 5555666677778888, IsBlocked = false, PinCode = 5678, Sum = 5000},
            new Card{Id = 6666777788889999, IsBlocked = false, PinCode = 6789, Sum = 6000},
            new Card{Id = 7777888899990000, IsBlocked = false, PinCode = 7890, Sum = 7000},
            new Card{Id = 8888999900001111, IsBlocked = false, PinCode = 8901, Sum = 8000},
            new Card{Id = 9999000011112222, IsBlocked = false, PinCode = 9012, Sum = 9000},
            new Card{Id = 0000111122223333, IsBlocked = false, PinCode = 0123, Sum = 0},
            new Card{Id = 0000222244446666, IsBlocked = false, PinCode = 0246, Sum = 0},
            new Card{Id = 2222444466668888, IsBlocked = false, PinCode = 2468, Sum = 2222},
            new Card{Id = 4444666688880000, IsBlocked = false, PinCode = 4680, Sum = 4444},
            new Card{Id = 1111333355557777, IsBlocked = false, PinCode = 1357, Sum = 1111},
            new Card{Id = 3333555577779999, IsBlocked = false, PinCode = 3579, Sum = 3333},
            new Card{Id = 1234123412341234, IsBlocked = true, PinCode = 1234, Sum = 1234},
            new Card{Id = 2345234523452345, IsBlocked = true, PinCode = 2345, Sum = 2345},
            new Card{Id = 3456345634563456, IsBlocked = true, PinCode = 3456, Sum = 3456}
            };

            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();

            var options = new List<Action>
            {
            new Action{Id=1, Description = "Просмотр баланса"},
            new Action{Id=2, Description = "Снятие денег"}
            };

            options.ForEach(o => context.Actions.Add(o));
            context.SaveChanges();
        }
    }
}