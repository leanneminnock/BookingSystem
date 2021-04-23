using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data.Entities
{
    public class Hotel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float DoublePrice { get; set; }
        public float TwinPrice { get; set; }
        public float KingPrice { get; set; }
        public float PenthousePrice { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Booking> Bookings { get; set; }


    }
}
