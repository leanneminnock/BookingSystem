using AutoMapper;
using BookingSystem.Data;
using BookingSystem.Data.Entities;
using BookingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _repo;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingViewModel>> GetAllBookings()
        {
            var bookings = _repo.GetAllBookings();
            try
            {
                return Ok(_mapper.Map<IEnumerable<BookingViewModel>>(bookings));
            }
            catch(NullReferenceException ex)
            {
                return BadRequest($"Could not fetch bookings {ex}");
            }

        }

        [HttpGet("{id}")]
        public ActionResult<BookingViewModel> GetBookingById(int id, bool includeItems)
        {
            try
            {
                var booking = _repo.GetBookingById(id, includeItems);
                return Ok(_mapper.Map<BookingViewModel>(booking));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No Booking matching Id {id}");
            }
        }

        [HttpGet("{id}/Booking")]
        public ActionResult<BookingViewModel> GetBookingByCustomerId(int id, bool includeItems)
        {
            try
            {
                var booking = _repo.GetBookingsByCustomerId(id, includeItems);
                return Ok(_mapper.Map<BookingViewModel>(booking));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No customer matching Id {id}");
            }
        }
        
        [HttpGet("{id}/Hotel")] // I know this is wrong but not sure what it should be
        public ActionResult<BookingViewModel> GetBookingsByHotelId(int id, bool includeItems)
        {
            try
            {
                var booking = _repo.GetBookingsByHotelId(id, includeItems);
                return Ok(_mapper.Map<BookingViewModel>(booking));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No hotel matching Id {id}");
            }
        }

        [HttpGet("{id}/Customer")] // I know this is wrong but not sure what it should be
        public ActionResult<CustomerViewModel> GetCustomerByBookingId(int id, bool includeItems)
        {
            try
            {
                var customer = _repo.GetCustomerByBookingId(id, includeItems);
                return Ok(_mapper.Map<CustomerViewModel>(customer));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No booking matching Id {id}");
            }
        }

        
        [HttpPost]
        public ActionResult<BookingViewModel> CreateBooking([FromBody] BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                Booking b = _mapper.Map<Booking>(booking);
                _repo.CreateNewBooking(b);
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<BookingViewModel>(b));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<BookingViewModel> UpdateBooking([FromBody] BookingViewModel booking, bool includeItems)
        {
            try
            {
                Booking b = _mapper.Map<Booking>(booking);
                if (!_repo.UpdateBooking(b, includeItems)) { return BadRequest(); }
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<BookingViewModel>(b));
            }
            catch(ArgumentException ex)
            {
                return NotFound($"could not find the requested booking {ex}");
            }
        }

        [HttpDelete]
        public ActionResult DeleteBookingById(int id, bool includeItems)
        {
            try
            {
                if (!_repo.DeleteBookingById(id, includeItems)) { return NotFound("Booking Id Not Valid."); }
                if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
                return Ok("Booking Deleted");
            }
            catch(ArgumentException ex)
            {
                return NotFound($"could not find the requested booking {ex}");
            }
            
        }
    }
}
