using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalReception.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalReception.DAL.Initializer
{
    public class HospitalReceptionInitializer : CreateDatabaseIfNotExists<HospitalReceptionDbContext>
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
                        FirstName = "Maxim",
                        LastName = "Ivanov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 1,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 1,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Natalia",
                        LastName = "Ivanova",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 2
                    },
                    new Doctor
                    {
                        FirstName = "Ivan",
                        LastName = "Petrov",
                        Rating = 3.8,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 3
                    },
                    new Doctor
                    {
                        FirstName = "Ivan",
                        LastName = "Alekseev",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 4,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Petr",
                        LastName = "Alekseev",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 5
                    },
                    new Doctor
                    {
                        FirstName = "Alexander",
                        LastName = "Petrov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 6,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Tatiana",
                        LastName = "Ivanova",
                        Rating = 4.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 7,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Nataila",
                        LastName = "Alekseeva",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 8
                    },
                    new Doctor
                    {
                        FirstName = "Alexey",
                        LastName = "Petrov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 9,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Grigorov",
                        LastName = "Valery",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 10,
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor"
                    },
                    new Doctor
                    {
                        FirstName = "Alexander",
                        LastName = "Abramson",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 11,
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                    },
                    new Doctor
                    {
                        FirstName = "John",
                        LastName = "Robins",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 1,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 12,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Alexander",
                        LastName = "Smith",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 1,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 13,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Adam",
                        LastName = "Green",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 14
                    },
                    new Doctor
                    {
                        FirstName = "James",
                        LastName = "Colins",
                        Rating = 3.8,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 15
                    },
                    new Doctor
                    {
                        FirstName = "Bill",
                        LastName = "White",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 16,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Sam",
                        LastName = "Mitchell",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 17
                    },
                    new Doctor
                    {
                        FirstName = "George",
                        LastName = "Adamson",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 18,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Anna",
                        LastName = "Ivanova",
                        Rating = 4.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 19,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Nataila",
                        LastName = "Alekseeva",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 20
                    },
                    new Doctor
                    {
                        FirstName = "Aleksey",
                        LastName = "Petrov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 21,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Valery",
                        LastName = "Grigorov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 22,
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor"
                    },
                    new Doctor
                    {
                        FirstName = "Alexander",
                        LastName = "Angular",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 23,
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                    },
                    new Doctor
                    {
                        FirstName = "Anastasia",
                        LastName = "Alekseeva",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 3,
                        CreationDate = new DateTime(2018, 5, 18),
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
                        ImageId = 24
                    },
                    new Doctor
                    {
                        FirstName = "Aleksey",
                        LastName = "Petrov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 4,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 25,
                        Education = "Voronezh State University",
                        Position = "Intern"
                    },
                    new Doctor
                    {
                        FirstName = "Alexey",
                        LastName = "Grigorov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 5,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 26,
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor"
                    },
                    new Doctor
                    {
                        FirstName = "Alexander",
                        LastName = "Alexandrov",
                        Rating = 5.0,
                        CreatedUserId = userId,
                        DepartmentId = 2,
                        CreationDate = new DateTime(2018, 5, 18),
                        ImageId = 27,
                        Education = "Voronezh Medical Academy",
                        Position = "Doctor",
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
                
                var consultationHours = new List<ConsultationHours>
                {
                    new ConsultationHours
                    {
                        DoctorId = 1,
                        DayOfWeek = DayOfWeek.Monday,
                        StartHour = 9,
                        StartMinutes = 0,
                        EndHour = 15,
                        EndMinutes = 30
                    },
                    new ConsultationHours
                    {
                        DoctorId = 1,
                        DayOfWeek = DayOfWeek.Tuesday,
                        StartHour = 15,
                        StartMinutes = 0,
                        EndHour = 21,
                        EndMinutes = 0
                    },
                    new ConsultationHours
                    {
                        DoctorId = 1,
                        DayOfWeek = DayOfWeek.Wednesday,
                        StartHour = 13,
                        StartMinutes = 30,
                        EndHour = 17,
                        EndMinutes = 0
                    },
                    new ConsultationHours
                    {
                        DoctorId = 2,
                        DayOfWeek = DayOfWeek.Thursday,
                        StartHour = 9,
                        StartMinutes = 0,
                        EndHour = 21,
                        EndMinutes = 0
                    },
                    new ConsultationHours
                    {
                        DoctorId = 2,
                        DayOfWeek = DayOfWeek.Friday,
                        StartHour = 15,
                        StartMinutes = 30,
                        EndHour = 21,
                        EndMinutes = 0
                    },
                    new ConsultationHours
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
                context.ConsultationHours.AddRange(consultationHours);
                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}