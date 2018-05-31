using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalReception.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalReception.DAL.Initializer
{
    public class HospitalReceptionInitializer : DropCreateDatabaseIfModelChanges<HospitalReceptionDbContext>
    {
        protected override void Seed(HospitalReceptionDbContext context)
        {
            var path = AppDomain.CurrentDomain.RelativeSearchPath;

            if (!context.Users.Any(u => u.UserName == "test@login.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "test@login.com" };

                manager.Create(user, "123456Abc*");

                var userId = context.Users.FirstOrDefault()?.Id;

                var departments = new List<Department>
                {
                    new Department {Name = "Therapy"},
                    new Department {Name = "Cardiology"},
                    new Department {Name = "Surgery"},
                    new Department {Name = "Neurology"},
                    new Department {Name = "Pediatrics"},
                };

                context.Depatments.AddRange(departments);

                var doctors = new List<Doctor>
                {
                    new Doctor
                    {
                        FirstName = "Ivanov",
                        LastName = "Alexander",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 1,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 1
                    },
                    new Doctor
                    {
                        FirstName = "Ivanova",
                        LastName = "Natalia",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 2
                    },
                    new Doctor
                    {
                        FirstName = "Petrov",
                        LastName = "Ivan",
                        Rating = 3.8,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 3
                    },
                    new Doctor
                    {
                        FirstName = "Alekseev",
                        LastName = "Ivan",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 4
                    },
                    new Doctor
                    {
                        FirstName = "Alekseev",
                        LastName = "Petr",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 5
                    },
                    new Doctor
                    {
                        FirstName = "Petrov",
                        LastName = "Alexander",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 6
                    },
                    new Doctor
                    {
                        FirstName = "Ivanova",
                        LastName = "Anna",
                        Rating = 4.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 7
                    },
                    new Doctor
                    {
                        FirstName = "Alekseeva",
                        LastName = "Nataila",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 8
                    },
                    new Doctor
                    {
                        FirstName = "Petrov",
                        LastName = "Aleksey",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 9
                    },
                    new Doctor
                    {
                        FirstName = "Grigorov",
                        LastName = "Valery",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 10
                    },
                    new Doctor
                    {
                        FirstName = "A",
                        LastName = "Alexander",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 11
                    }
                };

                context.Doctors.AddRange(doctors);
                context.SaveChanges();
            }
        }
    }
}