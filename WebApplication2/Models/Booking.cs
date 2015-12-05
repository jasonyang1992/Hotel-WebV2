using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Booking
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string GuestName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Room Number")]
        public string RoomNum { get; set; }
        [Display(Name = "Date In")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateIn { get; set; }
        [Display(Name = "Date Out")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOut { get; set; }
        [Display(Name = "Number of Adults")]
        public int NumberOfAdults { get; set; }
        [Display(Name = "Number of Kids")]
        public int NumberOfKids { get; set; }
        [Display(Name = "Grand Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal GrandTotal { get; set; }
        [Display(Name = "Total Paid")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalPaid { get; set; }
        [Display(Name = "Balance")]
        private decimal _balance;
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Balance
        {
            get
            {
                return GrandTotal - TotalPaid;
            }
            set { }
        }
    }

    public class BookingDBContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
    }
}
