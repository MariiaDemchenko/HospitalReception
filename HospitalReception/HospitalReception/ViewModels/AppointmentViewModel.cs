using HospitalReception.Converters;
using Newtonsoft.Json;
using System;

namespace HospitalReception.ViewModels
{
    public class AppointmentViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "patientId")]
        public int PatientId { get; set; }
        [JsonProperty(PropertyName = "patientFullName")]
        public string PatientFullName { get; set; }
        [JsonProperty(PropertyName = "doctorId")]
        public int DoctorId { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "start_date")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime StartDate { get; set; }
        [JsonProperty(PropertyName = "end_date")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime EndDate { get; set; }
    }
}