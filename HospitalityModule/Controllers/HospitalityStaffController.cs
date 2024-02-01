using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Errors;
using HospitalityModules.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalityModules.Controllers
{

    public class HospitalityStaffController : ApiBaseController
    {
        private readonly HospitalitystaffServices _services;

        public HospitalityStaffController(HospitalitystaffServices services)
        {
            _services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<HospitalityStaff>> GetAllStaff()
        {
            var Staff = _services.GetAllHospitalityStaff();
            return Ok(Staff);

        }


        [HttpGet("{id}")]
        public ActionResult<HospitalityStaff> GetpersonById(int id)
        {

            var person = _services.GetStaffById(id);
            if (person == null)
                return NotFound(new ApiResponse(404));
            return Ok(person);

        }


        [HttpPost]
        public ActionResult AddPerson([FromBody] HospitalityStaffDto staffDto)
        {
            _services.AddPerson(staffDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePersonById(int id)
        {
            _services.RemovePerson(id);
            return Ok();
        }
    }
}