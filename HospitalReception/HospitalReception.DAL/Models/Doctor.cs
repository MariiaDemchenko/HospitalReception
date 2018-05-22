﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalReception.DAL.Models
{
    public class Doctor : IEntityBase
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Rating { get; set; }
        [ForeignKey("User")]
        public string CreatedUserId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}