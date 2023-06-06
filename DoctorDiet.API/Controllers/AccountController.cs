using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using System;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private IMapper _mapper;
        private AccountService _accountService;
        IUnitOfWork _unitOfWork;
        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration,IMapper mapper, AccountService accountService, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            _mapper=mapper;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("PatientRegister")]
        public async Task<IActionResult> PatientRegister([FromForm]RegisterPatientDto registerPatientDto)
        {
            if (ModelState.IsValid)
            { 
                using var dataStream=new MemoryStream();
                registerPatientDto.ProfileImage.CopyTo(dataStream);

                ApplicationUser ApplicationUser = _mapper.Map<ApplicationUser>(registerPatientDto);
                ApplicationUser.ProfileImage=dataStream.ToArray();

                IdentityResult result = await userManager.CreateAsync(ApplicationUser, registerPatientDto.Password);
                RegisterDto registerDto = new RegisterDto();

                if (result.Succeeded)
                {


                    await userManager.AddToRoleAsync(ApplicationUser, "Patient");
                    Patient patient = new Patient();

                    patient.Id = ApplicationUser.Id;
                    patient.FullName=registerPatientDto.FullName;
                    patient.Gender = registerPatientDto.Gender;
                    patient.Height = registerPatientDto.Height;
                    patient.Weight = registerPatientDto.Weight;
                    patient.Goal = registerPatientDto.Goal;
                    patient.BirthDate = registerPatientDto.BirthDate;
                    patient.Diseases = registerPatientDto.Diseases;
                    patient.ApplicationUser= ApplicationUser;
                   


                    _accountService.AddPatient(patient);
                    _unitOfWork.CommitChanges();

                    registerDto.Message = "Success";
                    return Ok(registerDto);
                }
                else
                    registerDto.Message = "Failed";
                return BadRequest(registerDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("DoctorRegister")]
        public async Task<IActionResult> DoctorRegister([FromForm]RegisterDoctorDto registerDoctorDto)
        {
            if (ModelState.IsValid)
            {
                using var dataStream=new MemoryStream();
                registerDoctorDto.ProfileImage.CopyTo(dataStream);

                ApplicationUser ApplicationUser = _mapper.Map<ApplicationUser>(registerDoctorDto);
                ApplicationUser.ProfileImage=dataStream.ToArray();

                IdentityResult result = await userManager.CreateAsync(ApplicationUser, registerDoctorDto.Password);
                RegisterDto registerDto = new RegisterDto();

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(ApplicationUser, "Doctor");
                    Doctor doctor = new Doctor();
                    doctor.Id = ApplicationUser.Id;
                    doctor.FullName=registerDoctorDto.FullName;
                    doctor.ApplicationUser= ApplicationUser;            
                    doctor.Specialization = registerDoctorDto.Specialization;
                    doctor.Location = registerDoctorDto.Location;
                    doctor.ContactInfo= registerDoctorDto.ContactInfo;

                    _accountService.AddDoctor(doctor);
                    _unitOfWork.CommitChanges();

                    registerDto.Message = "Success";
                    return Ok(registerDto);
                }
                else
                    registerDto.Message = "Failed";
                return BadRequest(registerDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpPost("AdminRegister")]
        public async Task<IActionResult> AdminRegister([FromForm]RegisterAdminDto registerAdminDto)
        {
            if (ModelState.IsValid)
            {
                using var dataStream = new MemoryStream();
                registerAdminDto.ProfileImage.CopyTo(dataStream);

                ApplicationUser ApplicationUser = _mapper.Map<ApplicationUser>(registerAdminDto);
                ApplicationUser.ProfileImage=dataStream.ToArray();

                IdentityResult result = await userManager.CreateAsync(ApplicationUser, registerAdminDto.Password);
                RegisterDto registerDto = new RegisterDto();

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(ApplicationUser, "Admin");
                    Admin admin = new Admin();
                    admin.Id = ApplicationUser.Id;
                    admin.FullName= registerAdminDto.FullName;
                    admin.ApplicationUser = ApplicationUser;
                    
                   


                    _accountService.AddAdmin(admin);
                    _unitOfWork.CommitChanges();

                    registerDto.Message = "Success";
                    return Ok(registerDto);
                }
                else
                    registerDto.Message = "Failed";
                return BadRequest(registerDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(userDto.UserName);//.FindByNameAsync(userDto.UserName);
                LoginDto loginDto = new LoginDto();
                if (applicationUser != null && await userManager.CheckPasswordAsync(applicationUser, userDto.Password))
                {

                    var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                    SigningCredentials credentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256);

                    List<Claim> myClaims = new List<Claim>();

                    myClaims.Add(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id));
                    myClaims.Add(new Claim(ClaimTypes.Name, applicationUser.UserName));
                    myClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    JwtSecurityToken MyToken = new JwtSecurityToken(
                        issuer: configuration["JWT:ValidIssuer"],
                        audience: configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(6),
                        claims: myClaims,


                        signingCredentials: credentials
                        );
                    loginDto.Message = "Success";

                    return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                            expiration = MyToken.ValidTo,
                            Messege = "Success"
                        });

                }
                else
                {
                    loginDto.Message = "Invalid UserName";
                    return BadRequest(loginDto);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

}

