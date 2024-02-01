using HospitalityModules.Data;
using HospitalityModules.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalityModules.Controllers
{

    public class ErrorController : ApiBaseController
    {
        private readonly AppDbContext _dbContext;

        public ErrorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotfound()
        {
            var patient = _dbContext.Patients.Find(100);
            if (patient == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return NotFound();
        }
        [HttpGet("servererror")]
        public ActionResult GetServererror()
        {
            var patient = _dbContext.Patients.Find(100);
            var Returnpatient = patient.ToString();
            return Ok(Returnpatient);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadrequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadrequest(int Id)
        {
            return Ok();
        }
    }
}
