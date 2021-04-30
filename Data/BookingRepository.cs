using BookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
            List<Booking> bookings = new List<Booking>();
            bookings.AddRange(_ctx.Bookings.Include(b => b.Room).Include(b => b.Hotel));
            if (bookings == null)
            {
                throw new ArgumentException("Could not fetch all bookings, returned null.");
            }
            else
            {
                return bookings;
            }
        }
        public Booking GetBookingById(int bookingId, bool includeItems)
        {
           Booking booking = _ctx.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if(booking == null)
            {
                throw new ArgumentException($"No booking matching the id {bookingId}");
            }
            else
            {
                return booking;
            }
        }

        public Room GetRoomByBookingId(int bookingId, bool includeItems)
        {
            Booking booking = GetBookingById(bookingId, includeItems);
            if (booking == null)
            {
                throw new ArgumentException($"No booking matching the id {bookingId}");
            }
            else
            {
                return booking.Room;
            }
        }

        public IEnumerable<Booking> GetBookingsByCustomerId(int customerId, bool includeItems)
        {
            List<Booking> bookings = new List<Booking>(_ctx.Bookings.Where(b => b.Customer.Id == customerId));
            if (bookings == null)
            {
                throw new ArgumentException($"Could not fetch bookings for given customer {customerId}, returned null.");
            }
            else
            {
                return bookings;
            }
        }

        public IEnumerable<Booking> GetBookingsByHotelId(int hotelId, bool includeItems)
        {
            List<Booking> bookings = new List<Booking>(_ctx.Bookings.Where(b => b.Hotel.Id == hotelId));
            if (bookings == null)
            {
                throw new ArgumentException($"Could not fetch bookings for the Hotel {hotelId}, returned null.");
            }
            else
            {
                return bookings;
            }
        }

        public Customer GetCustomerByBookingId(int bookingId, bool includeItems)
        {
            Booking b = GetBookingById(bookingId, includeItems);
            if (b == null)
            {
                throw new ArgumentException($"Could not fetch Customer for the given booking Id{bookingId}, returned null.");
            }
            else
            {
                return b.Customer;
            }
        }

        public void CreateNewBooking(Booking newBooking)
        {
            _ctx.Bookings.Add(newBooking);
        }

        public bool DeleteBookingById(int bookingId, bool includeItems)
        {
            Booking booking = GetBookingById(bookingId, includeItems);
            if (booking == null)
            {
                throw new ArgumentException($"Could not fetch Booking for the given booking Id{bookingId}, returned null.");
            }
            _ctx.Bookings.Remove(booking);
            return true;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

       
        public bool UpdateBooking(Booking updateBooking, bool includeItems)
        {
            Booking currentBooking = GetBookingById(updateBooking.Id, includeItems);
            if (currentBooking == null)
            {
                throw new NullReferenceException($"Could not update Booking at Id{updateBooking.Id}, returned null.");
            }
            else
            {
                _ctx.Bookings.Remove(currentBooking);
                _ctx.Bookings.Add(updateBooking);
                return true;
            }
           
        }

    }
}
