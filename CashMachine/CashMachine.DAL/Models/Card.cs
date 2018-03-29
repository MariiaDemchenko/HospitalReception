using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashMachine.DAL.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public bool IsBlocked { get; set; }
        public string PinCodeHash { get; set; }
        public string PinCodeSalt { get; set; }
        public int Sum { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}