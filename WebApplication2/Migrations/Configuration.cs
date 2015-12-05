namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication2.Models.BookingDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication2.Models.BookingDBContext context)
        {
            context.Bookings.AddOrUpdate(i => i.GuestName,
                new Booking
                {
                    GuestName = "Mel Romero",
                    Address = "123 Lexington Ave",
                    City = "New York",
                    ZipCode = "10010",
                    Email = "mel.romero@sigmabaruch.org",
                    PhoneNumber = "3474042999",
                    RoomNum = "1",
                    DateIn = DateTime.Parse("10-01-2015"),
                    DateOut = DateTime.Parse("10-05-2015"),
                    NumberOfAdults = 1,
                    NumberOfKids = 0,
                    GrandTotal = 100.00M,
                    TotalPaid = 80.00M,
                    Balance = 20.00M

                }

           );

        }
    }
}

