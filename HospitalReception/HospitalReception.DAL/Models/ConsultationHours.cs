using System;

namespace HospitalReception.DAL.Models
{
    /// <summary>
    /// модель приемных часов доктора
    /// </summary>
    public class ConsultationHours : IEntityBase
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int StartHour { get; set; }
        public int StartMinutes { get; set; }
        public int EndHour { get; set; }
        public int EndMinutes { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}