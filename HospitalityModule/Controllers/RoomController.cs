using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Errors;
using HospitalityModules.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityModules.Controllers
{

    public class RoomController : ApiBaseController
    {
        private readonly RoomServices _roomServices;

        public RoomController(RoomServices roomServices)
        {
            _roomServices = roomServices;

        }
        [HttpPut("{RoomId}/Allocate/{PatientId}")]
        public ActionResult AllocateRoom(int RoomId, int PatientId)
        {
            var allocatedRoom = _roomServices.AllocateRoom(RoomId, PatientId);
            if (allocatedRoom <= 0) return BadRequest(new ApiResponse(400));
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomDto>> GetAllRooms()
        {
            var room = _roomServices.GetAllRooms();
            return Ok(room);
        }

        [HttpGet("{id}")]
        public ActionResult<RoomDto> GetRoomById(int id)
        {
            var patient = _roomServices.GetRoomById(id);

            if (patient == null)
                return NotFound(new ApiResponse(404));

            return Ok(patient);
        }

        [HttpPost]
        public ActionResult AddRoom([FromBody] RoomDto Room)
        {

            _roomServices.AddRoom(Room);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteRoom(int id)
        {
            _roomServices.RemoveRoom(id);
            return Ok();
        }
    }
}
