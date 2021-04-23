using BookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
            
        }

       public DbSet<Customer> Customers { get; set; }
       public DbSet<Booking> Bookings { get; set; }
       public DbSet<Room> Rooms { get; set; }
       public DbSet<Hotel> Hotels { get; set; }

       

    }
}
