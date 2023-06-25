using DoctorDiet.Dto;
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

            public PatientController(PatientService patientService, IUnitOfWork unitOfWork)
            {
               _patientService = patientService;
               _unitOfWork = unitOfWork;
            }

        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {

            return Ok(_patientService.GetAllPatients());

        }


        [HttpGet("patientid")]
        public IActionResult GetPatientById(string patientid)
        {
            var patient = _patientService.GetPatientData(patientid);

            return Ok(patient);

        }
        [HttpGet("GetAllDaysOfSpecificCustomPlan{PatientId}")]
        public IActionResult GetAllDaysOfSpecificCustomPlan(string PatientId)
        {
            if (ModelState.IsValid)
            {
                List<List<DayCustomPlan>> DaysWithMeals = _patientService.GetEveryDayWithMealsOfDay(PatientId);
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

      [HttpPost("AddPatientNote")]
      public IActionResult AddNote(PatientNotesDTO patientNotesDto)
      {
        _patientService.AddNote(patientNotesDto);
        _unitOfWork.CommitChanges();
        return NoContent();
      }

      [HttpPost("GetPatientNotes")]
      public IActionResult GetNote(GetPatientNotesDTO getPatientNotesDTO)
      {
          List<PatientNotes> PatientNotes = _patientService.GetPateintNotes(getPatientNotesDTO);
          _unitOfWork.CommitChanges();
          return Ok(PatientNotes);
      }

        [HttpPut("ConfirmAccount")]
        public IActionResult ConfirmAccount(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
                string Status = _patientService.Confirm(subscribeDto);
                _unitOfWork.CommitChanges();

                return Ok(Status);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpPut("RejectAccount")]
        public IActionResult RejectAccount(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
                string Status = _patientService.Reject(subscribeDto);
                _unitOfWork.CommitChanges();

                return Ok(Status);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpPost("Subscribtion")]
        public IActionResult Subscribtion(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
               PatientDTO patientDTO= _patientService.Subscription(subscribeDto);
                _unitOfWork.CommitChanges();

                return Ok(patientDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }


        [HttpGet("patientDataDTO/{patientid}")]
        public IActionResult GetPatientDtoById(string patientid)
        {
            var patient = _patientService.GetPatientDataDTO(patientid);

            return Ok(patient);

        }

    [HttpGet("patientDTO/{patientid}")]
    public IActionResult GetPatientDto(string patientid)
    {
      var patient = _patientService.GetPatientDTO(patientid);

      return Ok(patient);

    }

    [HttpGet("GetPatientsByDoctorIdWithStatusConfirmed")]
        public IActionResult GetPatientsByDoctorIdWithStatusConfirmed(string Doctorid)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<PatientDTO> patients = _patientService.GetPatientsByDoctorIdWithStatusConfirmed(Doctorid);
                return Ok(patients);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetPatientsByDoctorIdWithStatusWaiting")]
        public IActionResult GetPatientsByDoctorIdWithStatusWaiting(string Doctorid)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<PatientDTO> patients = _patientService.GetPatientsByDoctorIdWithStatusWaiting(Doctorid);
                return Ok(patients);
            }
            else
            {
                return BadRequest(ModelState);
            }
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



    }
}
