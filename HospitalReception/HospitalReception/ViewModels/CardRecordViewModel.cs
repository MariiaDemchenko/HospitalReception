
namespace HospitalReception.ViewModels
{
    /// <summary>
    /// модель представления медицинской карты пациента
    /// </summary>
    public class CardRecordViewModel
    {
        public string Doctor { get; set; }
        public int DoctorId { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string AppointmentDate { get; set; }
    }
}