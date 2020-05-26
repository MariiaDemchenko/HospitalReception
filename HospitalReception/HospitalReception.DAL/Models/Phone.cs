namespace HospitalReception.DAL.Models
{
    public class Phone : IEntityBase
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description{ get; set; }
    }
}