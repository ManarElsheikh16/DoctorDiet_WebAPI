using DoctorDiet.DTO;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {

        DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        [HttpGet("doctorid")]
        public IActionResult GetDoctorById(string doctorid)
        {
            var doctor = _doctorService.GetDoctorData(doctorid);

            return Ok(doctor);

        }
        [HttpGet("GetAllDoctors")]
        public IActionResult GetAllDoctor() 
        {
            if(ModelState.IsValid)
            {
              IQueryable<RegisterDoctorDto> doctors= _doctorService.GetListOfDoctors();
                return Ok(doctors);
            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }
    }
}
