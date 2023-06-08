using DoctorDiet.Dto;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {

    private readonly UserManager<ApplicationUser> _userManager;

    DoctorService _doctorService;

        public DoctorController(DoctorService doctorService, UserManager<ApplicationUser> userManager)
        {
            _doctorService = doctorService;
      _userManager = userManager;
        }


        [HttpGet("doctorid")]
        public IActionResult GetDoctorById(string doctorid)
        {
            var doctor = _doctorService.GetDoctorData(doctorid);

            return Ok(doctor);

        }
    [HttpPost("ChangePassowrd")]
    public async Task<IActionResult> ChangePassowrd(DoctorNewPassowrd newPassowrd)
    {

      if (ModelState.IsValid)
      {
        Doctor doctor = _doctorService.GetDoctorData(newPassowrd.DoctorId);

        if (doctor != null)
        {
          var newPasswordHash = _userManager.PasswordHasher.HashPassword(doctor.ApplicationUser, newPassowrd.Password);

          var token = await _userManager.GeneratePasswordResetTokenAsync(doctor.ApplicationUser);
          IdentityResult result = await _userManager.ResetPasswordAsync(doctor.ApplicationUser, token, newPasswordHash);
          RegisterDto registerDto = new RegisterDto();

          if (result.Succeeded)
          {
            registerDto.Message = "Success";
            return Ok(registerDto);
          }
          else
            registerDto.Message = "Failed";
          return BadRequest(registerDto);
        }

      }
      return BadRequest(ModelState);

    }
  }
}
