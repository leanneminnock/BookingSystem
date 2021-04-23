using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoomById(int Id);
        Booking GetBookingByRoomId(int roomId);

        void CreateNewRoom(Room room);
        bool UpdateRoom(Room room);
        bool SaveAll();
        bool DeleteRoomById(int id);
    }
}
