using CacheMachine.Common;
using CacheMachine.DAL.Models;
using System.Collections.Generic;

namespace CacheMachine.DAL
{
    public class CacheMachineInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CacheMachineContext>
    {
        protected override void Seed(CacheMachineContext context)
        {
            var hashHelper = new HashcodeHelper();

            var cards = new List<Card>
            {
            new Card{Id = "1111222233334444", IsBlocked = false, Sum = 1000},
            new Card{Id = "2222333344445555", IsBlocked = false, Sum = 2000},
            new Card{Id = "3333444455556666", IsBlocked = false, Sum = 3000},
            new Card{Id = "4444555566667777", IsBlocked = false, Sum = 4000},
            new Card{Id = "5555666677778888", IsBlocked = false, Sum = 5000},
            new Card{Id = "6666777788889999", IsBlocked = false, Sum = 6000},
            new Card{Id = "7777888899990000", IsBlocked = false, Sum = 7000},
            new Card{Id = "8888999900001111", IsBlocked = false, Sum = 8000},
            new Card{Id = "9999000011112222", IsBlocked = false, Sum = 9000},
            new Card{Id = "0000111122223333", IsBlocked = false, Sum = 0},
            new Card{Id = "0000222244446666", IsBlocked = false, Sum = 0},
            new Card{Id = "2222444466668888", IsBlocked = false, Sum = 2222},
            new Card{Id = "4444666688880000", IsBlocked = false, Sum = 4444},
            new Card{Id = "1111333355557777", IsBlocked = false, Sum = 1111},
            new Card{Id = "3333555577779999", IsBlocked = false, Sum = 3333},
            new Card{Id = "1234123412341234", IsBlocked = true, Sum = 1234},
            new Card{Id = "2345234523452345", IsBlocked = true, Sum = 2345},
            new Card{Id = "3456345634563456", IsBlocked = true, Sum = 3456}
            };

            foreach (var card in cards)
            {
                var plainPinCode = GetRandomPinCode(card.Id);
                var hash = hashHelper.GenerateHash(plainPinCode, out var salt);
                card.PinCodeHash = hash;
                card.PinCodeSalt = salt;
            }

            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();

            var options = new List<Action>
            {
            new Action{Id=1, Description = Resources.ViewBalance},
            new Action{Id=2, Description = Resources.GetCache}
            };

            options.ForEach(o => context.Actions.Add(o));
            context.SaveChanges();
        }

        private string GetRandomPinCode(string id)
        {
            return string.Empty + id[0] + id[4] + id[8] + id[12];
        }
    }
}