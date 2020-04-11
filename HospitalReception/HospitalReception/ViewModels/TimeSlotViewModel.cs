using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalReception.Converters;
using Newtonsoft.Json;

namespace HospitalReception.ViewModels
{
    public class TimeSlotViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}