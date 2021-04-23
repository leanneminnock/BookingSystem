using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data.Entities
{
    public enum RoomType
    {
        Double,
        Twin, 
        King,
        Penthouse
    }
    public class Room
    {
        public int Id { get; set; }
        public int FloorNo { get; set; }
        public int RoomNo { get; set; }
        public double Price { get; set; }
        public RoomType RoomType { get; set; }
        public List<Booking> Bookings { get; set; }

    }
}
