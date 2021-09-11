using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalReception.DAL.Models
{
    public class Document : IEntityBase
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string IssueArea { get; set; }
        public DateTime IssueDate { get; set; }
        [ForeignKey("InsuranceCompany")]
        public int InsuranceCompanyId { get; set; }
        public bool IsDNS { get; set; }
        public string Description { get; set; }
        
        public virtual Patient Patient { get; set; }
        public virtual Organization InsuranceCompany { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}