using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.DAL.Models
{
    public class Measurement
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int ItemId { get; set; }

        public string Value { get; set; }
        
        public DateTime MeasurementDate { get; set; }

        public virtual Item Item { get; set; }
    }
}
