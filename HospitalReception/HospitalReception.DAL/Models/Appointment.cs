using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HospitalReception.DAL.Models
{
    /// <summary>
    /// модель визитов к доктору
    /// </summary>
    public class Appointment : IEntityBase
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "patientId")]
        public int? PatientId { get; set; }
        [JsonProperty(PropertyName = "doctorId")]
        public int DoctorId { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }
        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { get; set; }
        [ForeignKey("User")]
        public string CreatedUserId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}