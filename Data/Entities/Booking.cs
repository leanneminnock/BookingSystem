using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Data.Entities
{
    public class Booking
    {
       
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Room Room { get; set; }
        public Customer Customer { get; set; }
        public Hotel Hotel { get; set; }

    }
}
