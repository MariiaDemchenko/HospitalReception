using System;

namespace CashMachine.DAL.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string CardId { get; set; }
        public int ActionId { get; set; }
        public DateTime OperationDate { get; set; }
        public int? Sum { get; set; }

        public virtual Card Card { get; set; }
        public virtual Action Action { get; set; }
    }
}