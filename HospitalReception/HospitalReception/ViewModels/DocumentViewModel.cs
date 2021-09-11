using System;

namespace HospitalReception.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string IssueArea { get; set; }
        public DateTime IssueDate { get; set; }
        public int InsuranceCompanyId { get; set; }
        public string InsuranceCompany { get; set; }
        public bool IsDNS { get; set; }
        public string Description { get; set; }
    }
}