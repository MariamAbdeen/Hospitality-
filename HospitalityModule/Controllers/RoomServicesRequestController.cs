using AutoMapper;
using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityModules.Controllers
{

    public class RoomServicesRequestController : ApiBaseController
    {
        private readonly RoomServicesRequestServices _requestServices;
        private readonly IMapper _mapper;

        public RoomServicesRequestController(RoomServicesRequestServices requestServices, IMapper mapper)
        {
            _requestServices = requestServices;
            _mapper = mapper;
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _requestServices.DeleteServiceRequest(Id);
            return Ok();

        }

        [HttpGet("history/{patientId}")]
        public IActionResult GetServiceRequestHistory(int patientId)
        {
            var history = _requestServices.GetServiceRequestHistory(patientId);
            return Ok(history);
        }



        [HttpPut("update-status/{requestId}")]
        public IActionResult UpdateServiceRequestStatus(int requestId, [FromBody] string newStatus)
        {
            _requestServices.UpdateServiceRequestStatus(requestId, newStatus);
            return Ok();
        }

    }
}
