using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
        public List<BookingViewModel> Bookings { get; set; }
    }
}
