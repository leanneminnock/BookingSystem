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
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repo;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomViewModel>> GetAllRooms()
        {
            try
            {
                var rooms = _repo.GetAllRooms();
                return Ok(_mapper.Map<IEnumerable<RoomViewModel>>(rooms));
            }
            catch(NullReferenceException ex)
            {
                return BadRequest($"Could not fetch all rooms, {ex}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<RoomViewModel> GetRoomById(int id)
        {
            try
            {
                var room = _repo.GetRoomById(id);
                return Ok(_mapper.Map<RoomViewModel>(room));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No Room matching Id {id}, {ex}");
            }
        }

        [HttpPost]
        public ActionResult<RoomViewModel> CreateRoom([FromBody] Room room)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateNewRoom(room);
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<RoomViewModel>(room));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<RoomViewModel> UpdateRoom([FromBody] Room room)
        {
            try
            {
                if (!_repo.UpdateRoom(room)) { return BadRequest(); }

                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<RoomViewModel>(room));
            }
            catch(ArgumentException ex)
            {
                return BadRequest($"Could not find room of id {room.Id}, {ex}");
            }
        }

        [HttpDelete]
        public ActionResult DeleteRoomById(int id)
        {
            try
            {
                if (!_repo.DeleteRoomById(id)) { return NotFound("Room Id Not Valid."); }
                if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
                return Ok("Room Deleted");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Could not find room of id {id}, {ex}");
            }
        }
    }
}
