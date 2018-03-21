using System;

namespace CacheMachine.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public long CardId { get; set; }
        public int OptionId { get; set; }
        public DateTime OperationDate { get; set; }
        public int Sum { get; set; }

        public virtual Card Card { get; set; }
        public virtual Action Option { get; set; }
    }
}