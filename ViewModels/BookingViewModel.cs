using BookingSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RoomViewModel Room { get; set; }
        public CustomerViewModel Customer { get; set; }
        public HotelViewModel Hotel { get; set; }
    }
}
