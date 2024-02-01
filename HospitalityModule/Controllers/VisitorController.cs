using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Errors;
using HospitalityModules.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityModules.Controllers
{

    public class VisitorController : ApiBaseController
    {
        private readonly VisitorServices _visitorServices;

        public VisitorController(VisitorServices visitorServices)
        {
            _visitorServices = visitorServices;
        }

        [HttpPost]

        public ActionResult RegisterVisitor(int patientId, [FromBody] VisitorsDto visitor)
        {

            _visitorServices.RegisterVisitor(patientId, visitor);
            return Ok();
        }

        [HttpGet("{visitorId}")]
        public ActionResult<Visitor> GetVisitorDetails(int visitorId)
        {
            var visitor = _visitorServices.GetVisitorDetails(visitorId);
            if (visitor == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(visitor);
        }

        [HttpGet("{patientId}/history")]
        public ActionResult<IEnumerable<Visitor>> GetVisitorHistory(int patientId)
        {
            var visitorHistory = _visitorServices.GetVisitorHistory(patientId);
            if (visitorHistory == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(visitorHistory);
        }

        [HttpDelete]
        public ActionResult DeleteVisitor(int visitorId)
        {
            _visitorServices.DeleteVisitor(visitorId);
            return Ok();
        }
    }
}
