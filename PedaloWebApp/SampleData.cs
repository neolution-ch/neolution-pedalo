namespace PedaloWebApp
{
    using System;
    using System.Linq;
    using PedaloWebApp.Core.Domain;
    using PedaloWebApp.Core.Domain.Entities;
    using PedaloWebApp.Core.Interfaces.Data;

    public static class SampleData
    {
        public static void InitializePedaloDatabase(IDbContextFactory contextFactory)
        {
            using var context = contextFactory.CreateContext();
            if (!context.Database.EnsureCreated())
            {
                return;
            }

            if (!context.Bookings.Any())
            {
                // Only insert data if database was not seeded already
                InsertTestData(context);
            }
        }

        private static void InsertTestData(PedaloContext context)
        {
            var customer1 = new Customer
            {
                FirstName = "Wilhelm",
                LastName = "Tell",
                BirthdayDate = new DateTime(1970, 1, 1),
            };
            context.Customers.Add(customer1);

            var customer2 = new Customer
            {
                FirstName = "Arthur",
                LastName = "Dent",
                BirthdayDate = new DateTime(1970, 2, 1),
            };
            context.Customers.Add(customer2);

            var customer3 = new Customer
            {
                FirstName = "Ford",
                LastName = "Prefect",
                BirthdayDate = new DateTime(1970, 3, 1),
            };
            context.Customers.Add(customer3);

            var customer4 = new Customer
            {
                FirstName = "Zaphod",
                LastName = "Beeblebrox",
                BirthdayDate = new DateTime(1970, 4, 1),
            };
            context.Customers.Add(customer4);

            var customer5 = new Customer
            {
                FirstName = "Tricia",
                LastName = "McMillan",
                BirthdayDate = new DateTime(1970, 5, 1),
            };
            context.Customers.Add(customer5);

            var customer6 = new Customer
            {
                FirstName = "Luke",
                LastName = "Skywalker",
                BirthdayDate = new DateTime(1970, 6, 1),
            };
            context.Customers.Add(customer6);

            var customer7 = new Customer
            {
                FirstName = "Han",
                LastName = "Solo",
                BirthdayDate = new DateTime(1970, 7, 1),
            };
            context.Customers.Add(customer7);

            var customer8 = new Customer
            {
                FirstName = "Obiwan",
                LastName = "Kenobi",
                BirthdayDate = new DateTime(1970, 8, 1),
            };
            context.Customers.Add(customer8);
            context.SaveChanges();

            var pedalo1 = new Pedalo
            {
                Name = "Heart of Gold",
                Capacity = 6,
                Color = PedaloColor.Blue,
                HourlyRate = 21.00m,
            };
            context.Pedaloes.Add(pedalo1);

            var pedalo2 = new Pedalo
            {
                Name = "Golgafrincham",
                Capacity = 2,
                Color = PedaloColor.Pink,
                HourlyRate = 10.00m,
            };
            context.Pedaloes.Add(pedalo2);

            var pedalo3 = new Pedalo
            {
                Name = "Millennium Falcon",
                Capacity = 4,
                Color = PedaloColor.Green,
                HourlyRate = 17.00m,
            };
            context.Pedaloes.Add(pedalo3);

            var pedalo4 = new Pedalo
            {
                Name = "Flying Dutchman",
                Capacity = 4,
                Color = PedaloColor.Green,
                HourlyRate = 17.00m,
            };
            context.Pedaloes.Add(pedalo4);
            context.SaveChanges();

            context.Bookings.Add(new Booking { CustomerId = customer1.CustomerId, PedaloId = pedalo2.PedaloId, StartDate = new DateTime(2016, 7, 25, 09, 30, 00), EndDate = new DateTime(2016, 7, 25, 10, 30, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer2.CustomerId, PedaloId = pedalo1.PedaloId, StartDate = new DateTime(2016, 7, 25, 10, 10, 00), EndDate = new DateTime(2016, 7, 25, 10, 50, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer3.CustomerId, PedaloId = pedalo3.PedaloId, StartDate = new DateTime(2016, 7, 25, 11, 00, 00), EndDate = new DateTime(2016, 7, 25, 12, 10, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer4.CustomerId, PedaloId = pedalo2.PedaloId, StartDate = new DateTime(2016, 7, 25, 12, 15, 00), EndDate = new DateTime(2016, 7, 25, 13, 00, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer5.CustomerId, PedaloId = pedalo3.PedaloId, StartDate = new DateTime(2016, 7, 25, 12, 25, 00), EndDate = new DateTime(2016, 7, 25, 13, 05, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer6.CustomerId, PedaloId = pedalo3.PedaloId, StartDate = new DateTime(2016, 7, 25, 13, 10, 00), EndDate = new DateTime(2016, 7, 25, 14, 30, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer6.CustomerId, PedaloId = pedalo1.PedaloId, StartDate = new DateTime(2016, 7, 25, 15, 05, 00), EndDate = new DateTime(2016, 7, 25, 15, 55, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer7.CustomerId, PedaloId = pedalo1.PedaloId, StartDate = new DateTime(2016, 7, 25, 16, 20, 00), EndDate = new DateTime(2016, 7, 25, 17, 05, 00) });
            context.Bookings.Add(new Booking { CustomerId = customer2.CustomerId, PedaloId = pedalo3.PedaloId, StartDate = new DateTime(2016, 7, 25, 16, 30, 00), EndDate = new DateTime(2016, 7, 25, 17, 30, 00) });
            
            context.SaveChanges();
        }
    }
}
