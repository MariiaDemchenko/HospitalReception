﻿namespace HospitalReception.DAL.Models
{
    public class Gender : IEntityBase
    {
        public int Id { get; set; }
        public string NameLocal { get; set; }
        public string NameEng{ get; set; }
    }
}