using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace HospitalReception.DAL.Models
{
    public class Address : IEntityBase
    {
        public int Id { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public string PostCode { get; set; }
        [ForeignKey("LocalityType")]
        public int LocalityTypeId { get; set; }
        public string NameLocal { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Region Region { get; set; }
        public virtual Area Area { get; set; }
        public virtual LocalityType LocalityType { get; set; }
    }
}