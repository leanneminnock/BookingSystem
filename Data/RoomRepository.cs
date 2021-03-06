using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingContext _ctx;

        public RoomRepository(BookingContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>(_ctx.Rooms);
            if(rooms == null)
            {
                throw new NullReferenceException();
            }
            return rooms;
        }

        public Room GetRoomById(int id)
        {
            Room room = _ctx.Rooms.FirstOrDefault(r => r.Id == id);
            if(room == null)
            {
                throw new ArgumentException();
            }
            return room;
        }
        public void CreateNewRoom(Room room)
        {
            _ctx.Rooms.Add(room);
        }

        public bool DeleteRoomById(int id)
        {
            Room currentRoom = GetRoomById(id);
            if (currentRoom == null)
            {
                throw new ArgumentException();
            }
            _ctx.Rooms.Remove(currentRoom);
            return true;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public bool UpdateRoom(Room updatedRoom)
        {
            Room currentRoom = GetRoomById(updatedRoom.Id);
            if (currentRoom == null)
            {
                throw new ArgumentException();
            }
            _ctx.Rooms.Remove(currentRoom);
            _ctx.Rooms.Add(currentRoom);
            return true;
        }

        public Booking GetBookingByRoomId(int roomId)
        {
            return _ctx.Bookings.FirstOrDefault(b => b.Room.Id == roomId);
        }

    }
}
