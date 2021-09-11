using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalReception.ViewModels
{
    public class MeasurementViewModel
    {
        public string Label { get; set; }

        public string Value { get; set; }

        public string UnitName { get; set; }

        public DateTime MeasurementDate { get; set; }
    }
}