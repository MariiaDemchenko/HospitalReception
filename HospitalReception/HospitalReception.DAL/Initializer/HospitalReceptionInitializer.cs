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

                var patients = new List<Patient>
                {
                    new Patient
                    {
                        FirstName = "Ivanov",
                        LastName = "Alexander",
                        Email = "ivanov@account.com",
                        PhoneNumber = "71111111111",
                        BirthDate = new DateTime(2018, 1, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Ivanova",
                        LastName = "Natalia",
                        Email = "ivanova@account.com",
                        PhoneNumber = "72222222222",
                        BirthDate = new DateTime(2018, 2, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Petrov",
                        LastName = "Ivan",
                        Email = "petrov@account.com",
                        PhoneNumber = "73333333333",
                        BirthDate = new DateTime(2018, 3, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Alekseev",
                        LastName = "Ivan",
                        Email = "alekseev@account.com",
                        PhoneNumber = "74444444444",
                        BirthDate = new DateTime(2018, 4, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Alekseev",
                        LastName = "Petr",
                        Email = "alekseev@account2.com",
                        PhoneNumber = "75555555555",
                        BirthDate = new DateTime(2018, 5, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Petrov",
                        LastName = "Alexander",
                        Email = "petrov@account2.com",
                        PhoneNumber = "76666666666",
                        BirthDate = new DateTime(2018, 6, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Ivanova",
                        LastName = "Anna",
                        Email = "ivanova@account.com",
                        PhoneNumber = "77777777777",
                        BirthDate = new DateTime(2018, 7, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Alekseeva",
                        LastName = "Nataila",
                        Email = "alekseeva@account.com",
                        PhoneNumber = "78888888888",
                        BirthDate = new DateTime(2018, 8, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Petrov",
                        LastName = "Aleksey",
                        Email = "petrov@account3.com",
                        PhoneNumber = "79999999999",
                        BirthDate = new DateTime(2018, 9, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "Grigorov",
                        LastName = "Valery",
                        Email = "grigorov@account.com",
                        PhoneNumber = "71010101010",
                        BirthDate = new DateTime(2018, 10, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Patient
                    {
                        FirstName = "A",
                        LastName = "Alexander",
                        Email = "a@account.com",
                        PhoneNumber = "76666666666",
                        BirthDate = new DateTime(2018, 11, 18),
                        RegistrationDate = new DateTime(2018, 11, 18),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    }
                };

                context.Doctors.AddRange(doctors);
                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}