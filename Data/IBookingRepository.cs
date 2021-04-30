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
        Booking GetBookingById(int bookingId, bool includeItems);
        IEnumerable<Booking> GetBookingsByHotelId(int hotelId, bool includeItems);
        IEnumerable<Booking> GetBookingsByCustomerId(int customerId, bool includeItems);
        
        Customer GetCustomerByBookingId(int bookingId, bool includeItems);
        Room GetRoomByBookingId(int id, bool includeItems);

        void CreateNewBooking(Booking booking);
        bool UpdateBooking(Booking updatedBooking, bool includeItems);
        bool SaveAll();
        bool DeleteBookingById(int id, bool includeItems);
    }
}
