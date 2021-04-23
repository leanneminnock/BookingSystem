using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int bookingId);
        IEnumerable<Booking> GetBookingsByHotelId(int hotelId);
        IEnumerable<Booking> GetBookingsByCustomerId(int customerId);
        
        Customer GetCustomerByBookingId(int bookingId);
        Room GetRoomByBookingId(int id);

        void CreateNewBooking(Booking booking);
        bool UpdateBooking(Booking updatedBooking);
        bool SaveAll();
        bool DeleteBookingById(int id);
    }
}
