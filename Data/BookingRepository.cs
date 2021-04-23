using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingContext _ctx;

        public BookingRepository(BookingContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Booking> GetAllBookings()
        {
            return _ctx.Bookings;
        }
        public Booking GetBookingById(int bookingId)
        {
           return _ctx.Bookings.FirstOrDefault(b => b.Id == bookingId);
        }
        public Room GetRoomByBookingId(int id)
        {
            Booking booking = GetBookingById(id);
            return booking.Room;
        }
        public IEnumerable<Booking> GetBookingsByCustomerId(int customerId)
        {
            return _ctx.Bookings.Where(b => b.Customer.Id == customerId);
        }

        public IEnumerable<Booking> GetBookingsByHotelId(int hotelId)
        {
            return _ctx.Bookings.Where(b => b.Hotel.Id == hotelId);
        }

        
        public Customer GetCustomerByBookingId(int bookingId)
        {
            Booking b = GetBookingById(bookingId);
            if (b == null)
            {
                return null;
            }
            return b.Customer;
        }
        public void CreateNewBooking(Booking newBooking)
        {
            _ctx.Bookings.Add(newBooking);
        }

        public bool DeleteBookingById(int id)
        {
            Booking booking = GetBookingById(id);
            if (booking == null)
            {
                return false;
            }
            _ctx.Bookings.Remove(booking);
            return true;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

       
        public bool UpdateBooking(Booking updateBooking)
        {
            Booking currentBooking = GetBookingById(updateBooking.Id);
            if (currentBooking == null)
            {
                return false;
            }
            _ctx.Bookings.Remove(currentBooking);
            _ctx.Bookings.Add(updateBooking);
            return true;
        }

    }
}
