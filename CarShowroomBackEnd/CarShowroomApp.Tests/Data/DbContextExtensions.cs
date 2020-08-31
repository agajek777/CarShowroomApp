using CarShowroomApp.Data;
using CarShowroomApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroomApp.Tests.Data
{
    public static class DbContextExtensions
    {
        // Insert data to Unit Tests
        public static void Seed(this DatabaseContext db)
        {
            db.Cars.Add(
                new Car()
                {
                    Brand = "Mercedes",
                    Description = "Nice car",
                    Engine = "2.0",
                    ImagePath = "www.google.com",
                    Mileage = 12000,
                    Model = "A180",
                    Power = 200,
                    Price = 120000,
                    Production = new DateTime(2010, 4, 10)
                });

            db.Cars.Add(
                new Car()
                {
                    Brand = "Renault",
                    Description = "Nice car",
                    Engine = "3.0",
                    ImagePath = "www.youtube.com",
                    Mileage = 24000,
                    Model = "Koleos",
                    Power = 150,
                    Price = 9000,
                    Production = new DateTime(2012, 1, 11)
                });

            db.Cars.Add(
                new Car()
                {
                    Brand = "Audi",
                    Description = "Nice car",
                    Engine = "1.6",
                    ImagePath = "www.facebook.com",
                    Mileage = 200,
                    Model = "Q7",
                    Power = 350,
                    Price = 75000,
                    Production = new DateTime(2019, 12, 24)
                });

            db.Cars.Add(
                new Car()
                {
                    Brand = "Opel",
                    Description = "Nice car",
                    Engine = "2.0",
                    ImagePath = "www.linkedin.com",
                    Mileage = 218000,
                    Model = "Vivaro",
                    Power = 150,
                    Price = 45600,
                    Production = new DateTime(2014, 6, 14)
                });

            db.SaveChanges();
        }
    }
}
