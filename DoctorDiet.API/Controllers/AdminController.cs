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
    public class AdminController : Controller
    {

    private readonly UserManager<ApplicationUser> _userManager;
    public AdminService adminService;

        public AdminController(AdminService adminService, UserManager<ApplicationUser> userManager
         )
        {
            this.adminService = adminService;
           _userManager = userManager;
        }


        [HttpGet("Adminid")]
        public IActionResult GetAdminById(string Adminid)
        {
            var Admin = adminService.GetAdminData(Adminid);

            return Ok(Admin);

        }
        [HttpPost("ChangePassowrd")]
        public async Task<IActionResult> ChangePassowrd(AdminNewPassowrd newPassowrd) {

      if (ModelState.IsValid)
      {
        Admin admin = adminService.GetAdminData(newPassowrd.AdminId);

        if(admin != null)
        {
          var newPasswordHash = _userManager.PasswordHasher.HashPassword(admin.ApplicationUser,newPassowrd.Password);

          var token = await _userManager.GeneratePasswordResetTokenAsync(admin.ApplicationUser);
          IdentityResult result = await _userManager.ResetPasswordAsync(admin.ApplicationUser, token, newPasswordHash);
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
