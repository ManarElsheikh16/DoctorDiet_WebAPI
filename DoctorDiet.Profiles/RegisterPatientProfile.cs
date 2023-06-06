using AutoMapper;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakiny.Profiles
{
    public class RegisterPatientProfile:Profile
    {
        public RegisterPatientProfile()
        {
            CreateMap<RegisterPatientDto, ApplicationUser>()
            .ForMember(dst => dst.ProfileImage, opt => opt.Ignore());
        }
    }
}
