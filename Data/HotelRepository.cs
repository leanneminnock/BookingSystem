using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public class HotelRepository : IHotelRepository
    {
        private readonly BookingContext _ctx;

        public HotelRepository(BookingContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Hotel> GetHotels()
        {
            return _ctx.Hotels;
        }

        public Hotel GetHotelById(int id)
        {
           Hotel hotel = _ctx.Hotels.FirstOrDefault(h => h.Id == id);
            if(hotel == null)
            {
                throw new ArgumentException("The Hotel Id is not valid.");
            }
            return hotel;
        }

        public IEnumerable<Room> GetRoomsByHotelId(int id)
        {
            Hotel hotel = GetHotelById(id);
            if (hotel == null)
            {
                throw new ArgumentException("The Hotel Id is not valid.");
            }
            return hotel.Rooms;
        }

        public void CreateNewHotel(Hotel hotel)
        {
            _ctx.Hotels.Add(hotel);
        }

        public bool UpdateHotel(Hotel updatedHotel)
        {
            Hotel currentHotel = GetHotelById(updatedHotel.Id);
            if (currentHotel == null) return false;
            _ctx.Hotels.Remove(currentHotel);
            _ctx.Hotels.Add(currentHotel);
            return true;
        }
        public bool DeleteHotelById(int id)
        {
            Hotel hotel = GetHotelById(id);
            if(hotel == null)
            {
                return false;
            }
            _ctx.Hotels.Remove(hotel);
            return true;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

    }
}
