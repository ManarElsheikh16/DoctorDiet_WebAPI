using DoctorDiet.Models;
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
        [HttpGet("GetAllDaysOfSpecificCustomPlan{PatientId}")]
        public IActionResult GetAllDaysOfSpecificCustomPlan (string PatientId) 
        {
            if(ModelState.IsValid) 
            {
                List<List<Day>> DaysWithMeals = _patientService.GetEveryDayWithMealsOfDay(PatientId);
                return Ok(DaysWithMeals);
            }
            else
            {
                return BadRequest(ModelState);
            }
           

        }

        [HttpGet("GetPatientHistory{PatientId}")]
        public IActionResult GetPatientHistory(string PatientId)
        {
            if (ModelState.IsValid)
            {
                List<CustomPlan> customsPlans = _patientService.GetPatientHistory(PatientId);
                return Ok(customsPlans);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

    }
}