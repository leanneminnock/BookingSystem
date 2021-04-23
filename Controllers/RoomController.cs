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
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repo;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<Room>> GetAllRooms()
        {
            var rooms = _repo.GetAllRooms();
            return Ok(_mapper.Map<IEnumerable<Room>>(rooms));
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(int id)
        {
            try
            {
                var room = _repo.GetRoomById(id);
                return Ok(_mapper.Map<Room>(room));
            }
            catch (ArgumentException ex)
            {
                return NotFound($"No Room matching Id {id}");
            }
        }

        [HttpPost]
        public ActionResult<Room> CreateRoom([FromBody] Room room)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateNewRoom(room);
                if (!_repo.SaveAll()) { return BadRequest(); }
                return Ok(_mapper.Map<Room>(room));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public ActionResult<Room> UpdateRoom([FromBody] Room room)
        {
            if (!_repo.UpdateRoom(room)) { return BadRequest(); }

            if (!_repo.SaveAll()) { return BadRequest(); }
            return Ok(_mapper.Map<Room>(room));
        }

        [HttpDelete]
        public ActionResult DeleteRoomById(int id)
        {
            if (!_repo.DeleteRoomById(id)) { return NotFound("Room Id Not Valid."); }
            if (!_repo.SaveAll()) { return BadRequest("Changes could not be saved."); }
            return Ok("Room Deleted");
        }
    }
}
