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
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly IHotelRepository _repo;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<Hotel>> GetAllHotels()
        {
            var hotels = _repo.GetHotels();
            return Ok(_mapper.Map<IEnumerable<Hotel>>(hotels));
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotelById(int id)
        {
            try
            {
                var hotel = _repo.GetHotelById(id);
                return Ok(_mapper.Map<Hotel>(hotel));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No hotel matching Id {id}");
            }
        }

        [HttpGet("{id}/rooms")]
        public ActionResult<IEnumerable<Room>> GetRoomsByHotelId(int id)
        {
            try
            {
                var rooms = _repo.GetRoomsByHotelId(id);
                return Ok(_mapper.Map<IEnumerable<Room>>(rooms));
            }
            catch(ArgumentException ex)
            {
                return NotFound($"No hotel matching Id {id}");
            }
        }
        
        [HttpPost]
        public ActionResult<Hotel> CreateHotel([FromBody]Hotel hotel)
        { 
            if (ModelState.IsValid)
            {
                _repo.CreateNewHotel(hotel);
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<Hotel>(hotel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<Hotel> UpdateHotel([FromBody] Hotel hotel)
        {
            if (!_repo.UpdateHotel(hotel)) { return BadRequest(); } 

            if (!_repo.SaveAll()) { return BadRequest(); } 
            return Ok(_mapper.Map<Hotel>(hotel));
        }

        [HttpDelete]
        public ActionResult DeleteHotelById(int id)
        {
            if (!_repo.DeleteHotelById(id)) { return NotFound("Hotel Id Not Valid."); }
            if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
            return Ok("Hotel Deleted");
        }

    }
}
