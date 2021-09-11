namespace HospitalReception.ViewModels
{
    /// <summary>
    /// модель представления доктора
    /// </summary>
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public string ImageId { get; set; }
        public double Rating { get; set; }
        public string Position { get; set; }
        public string Education { get; set; }
        public string FullName { get; set; }
    }
}