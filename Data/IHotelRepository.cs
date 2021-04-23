using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetHotels();
        Hotel GetHotelById(int id);
        void CreateNewHotel(Hotel hotel);
        bool UpdateHotel(Hotel updatedBooking);
        IEnumerable<Room> GetRoomsByHotelId(int id);
        bool SaveAll();
        bool DeleteHotelById(int id);
    }
}
