using AutoMapper;
using BookingSystem.Data;
using BookingSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<Booking>> GetAllBookings()
        {
            var bookings = _repo.GetAllBookings();
            return Ok(_mapper.Map<IEnumerable<Booking>>(bookings));

        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(int id)
        {
            try
            {
                var booking = _repo.GetBookingById(id);
                return Ok(_mapper.Map<Booking>(booking));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No Booking matching Id {id}");
            }
        }

        [HttpGet("{id}/Booking")]
        public ActionResult<Booking> GetBookingByCustomerId(int id)
        {
            try
            {
                var booking = _repo.GetBookingsByCustomerId(id);
                return Ok(_mapper.Map<Booking>(booking));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No customer matching Id {id}");
            }
        }
        
        [HttpGet("{id}/Hotel")] // I know this is wrong but not sure what it should be
        public ActionResult<Booking> GetBookingsByHotelId(int id)
        {
            try
            {
                var booking = _repo.GetBookingsByHotelId(id);
                return Ok(_mapper.Map<Booking>(booking));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No hotel matching Id {id}");
            }
        }

        [HttpGet("{id}/Customer")] // I know this is wrong but not sure what it should be
        public ActionResult<Customer> GetCustomerByBookingId(int id)
        {
            try
            {
                var customer = _repo.GetCustomerByBookingId(id);
                return Ok(_mapper.Map<Customer>(customer));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No booking matching Id {id}");
            }
        }

        
        [HttpPost]
        public ActionResult<Booking> CreateBooking([FromBody] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateNewBooking(booking);
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<Booking>(booking));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<Booking> UpdateBooking([FromBody] Booking booking)
        {
            if (!_repo.UpdateBooking(booking)) { return BadRequest(); }
            if (!_repo.SaveAll()) { return BadRequest(); }
            return Ok(_mapper.Map<Booking>(booking));
        }

        [HttpDelete]
        public ActionResult DeleteBookingById(int id)
        {
            if (!_repo.DeleteBookingById(id)) { return NotFound("Booking Id Not Valid."); }
            if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
            return Ok("Booking Deleted");
        }
    }
}
