using HospitalityModules.DTOs;
using HospitalityModules.Entities;
using HospitalityModules.Errors;
using HospitalityModules.Services;
using Microsoft.AspNetCore.Mvc;



namespace HospitalityModules.Controllers
{

    public class PatientController : ApiBaseController
    {
        private readonly PatientServices _patientService;

        public PatientController(PatientServices patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAllPatients()
        {
            var patients = _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            var patient = _patientService.GetPatientById(id);

            if (patient == null)
                return NotFound(new ApiResponse(404));

            return Ok(patient);
        }

        [HttpPost]
        public ActionResult AddPatient([FromBody] PatientDto patient)
        {

            _patientService.AddPatient(patient);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePatient(int id)
        {
            _patientService.RemovePatient(id);
            return NoContent();
        }

    }
}
