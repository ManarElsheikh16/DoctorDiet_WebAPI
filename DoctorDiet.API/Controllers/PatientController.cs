using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
        {
            PatientService _patientService;
    IUnitOfWork _unitOfWork;
            public PatientController(PatientService patientService,IUnitOfWork unitOfWork)
            {
            _patientService = patientService;
      _unitOfWork = unitOfWork;
            }


        [HttpGet("patientid")]
        public IActionResult GetPatientById(string patientid)
        {
            var patient = _patientService.GetPatientData(patientid);

            return Ok(patient);

        }

    [HttpPut("PatientId")]
    public IActionResult UpdatePatient(string PatientId, RegisterPatientDto registerPatientDto, params string[] updatedProp)
    {


      if (ModelState.IsValid)
      {

        _patientService.UpdatePatient(PatientId, registerPatientDto, updatedProp);
        _unitOfWork.CommitChanges();

        return Ok(" Successfully Updated");
      }
      else
      {
        return BadRequest(ModelState);
      }

    }
    [HttpGet("DoctorId")]
    public IActionResult GetAllPatient(string DoctorId) {


      IEnumerable<Patient> patients = (IEnumerable<Patient>)_patientService.AllPatients(DoctorId);
      return Ok(patients);
      

    }


    }
}
