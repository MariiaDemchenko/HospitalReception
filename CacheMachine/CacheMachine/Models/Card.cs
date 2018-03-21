using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CacheMachine.Models
{
    public class Card
    {
        //TODO: Id хранить в String
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public bool IsBlocked { get; set; }
        public int PinCode { get; set; }
        public int Sum { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
    }
}