using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Category { get; set; }

        public string UnitName { get; set; }

        public double Mean { get; set; }

        public double Std { get; set; }
    }
}
