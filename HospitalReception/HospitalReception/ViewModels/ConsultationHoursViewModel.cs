namespace HospitalReception.ViewModels
{
    /// <summary>
    /// модель представления приемных часов врача
    /// </summary>
    public class ConsultationHoursViewModel
    {
        public int StartHour { get; set; }
        public int StartMinutes { get; set; }
        public int EndHour { get; set; }
        public int EndMinutes { get; set; }
        public int DayOfWeek { get; set; }
    }
}