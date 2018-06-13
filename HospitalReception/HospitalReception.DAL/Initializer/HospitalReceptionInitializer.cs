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
                
                var consultationHours = new List<ConsultaionHours>
                {
                    new ConsultaionHours
                    {
                        DoctorId = 1,
                        DayOfWeek = DayOfWeek.Monday,
                        StartHour = 9,
                        StartMinutes = 0,
                        EndHour = 15,
                        EndMinutes = 30
                    },
                    new ConsultaionHours
                    {
                        DoctorId = 1,
                        DayOfWeek = DayOfWeek.Tuesday,
                        StartHour = 15,
                        StartMinutes = 0,
                        EndHour = 21,
                        EndMinutes = 0
                    },
                    new ConsultaionHours
                    {
                        DoctorId = 1,
                        DayOfWeek = DayOfWeek.Wednesday,
                        StartHour = 13,
                        StartMinutes = 30,
                        EndHour = 17,
                        EndMinutes = 0
                    },
                    new ConsultaionHours
                    {
                        DoctorId = 2,
                        DayOfWeek = DayOfWeek.Thursday,
                        StartHour = 9,
                        StartMinutes = 0,
                        EndHour = 21,
                        EndMinutes = 0
                    },
                    new ConsultaionHours
                    {
                        DoctorId = 2,
                        DayOfWeek = DayOfWeek.Friday,
                        StartHour = 15,
                        StartMinutes = 30,
                        EndHour = 21,
                        EndMinutes = 0
                    },
                    new ConsultaionHours
                    {
                        DoctorId = 3,
                        DayOfWeek = DayOfWeek.Saturday,
                        StartHour = 9,
                        StartMinutes = 0,
                        EndHour = 15,
                        EndMinutes = 0
                    }
                };

                var appointments = new List<Appointment>
                {
                    new Appointment
                    {
                        DoctorId = 1,
                        PatientId = 1,
                        StartDate = new DateTime(2018, 6, 4, 9, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 4, 9, 30, 0, DateTimeKind.Local),
                        Description = "Regular consultation",
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 1,
                        PatientId = 2,
                        Description = "Full observation",
                        StartDate = new DateTime(2018, 6, 4, 10, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 4, 13, 0, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 1,
                        PatientId = 3,
                        Description = "Operation",
                        StartDate = new DateTime(2018, 6, 5, 18, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 5, 20, 0, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 2,
                        PatientId = 1,
                        Description = "Basic Consultation",
                        StartDate = new DateTime(2018, 6, 7, 15, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 7, 15, 30, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 2,
                        PatientId = 2,
                        Description = "Some consultation",
                        StartDate = new DateTime(2018, 6, 8, 15, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 8, 17, 0, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 2,
                        PatientId = 1,
                        Description = "Procedures",
                        StartDate = new DateTime(2018, 6, 8, 17, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 8, 18, 30, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 2,
                        PatientId = 3,
                        Description = "Procedures",
                        StartDate = new DateTime(2018, 6, 8, 18, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 8, 19, 30, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 2,
                        PatientId = 4,
                        Description = "Procedures",
                        StartDate = new DateTime(2018, 6, 8, 19, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 8, 20, 30, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    },
                    new Appointment
                    {
                        DoctorId = 2,
                        PatientId = 5,
                        Description = "Procedures",
                        StartDate = new DateTime(2018, 6, 8, 20, 0, 0, DateTimeKind.Local),
                        EndDate = new DateTime(2018, 6, 8, 21, 30, 0, DateTimeKind.Local),
                        CreatedUserId = userId,
                        CreationDate = new DateTime(2018, 5, 18)
                    }
                };

                context.Doctors.AddRange(doctors);
                context.Patients.AddRange(patients);
                context.SaveChanges();
                context.ConsultaionHours.AddRange(consultationHours);
                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}