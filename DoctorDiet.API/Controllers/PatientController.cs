using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
        {
            PatientService _patientService;

            public PatientController(PatientService patientService)
            {
            _patientService = patientService;
            }


        [HttpGet("patientid")]
        public IActionResult GetPatientById(string patientid)
        {
            var patient = _patientService.GetPatientData(patientid);

            return Ok(patient);

        }
    }
}
