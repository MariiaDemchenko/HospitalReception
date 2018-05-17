namespace HospitalReception.DAL.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Rating { get; set; }

        public virtual Department Department { get; set; }
    }
}