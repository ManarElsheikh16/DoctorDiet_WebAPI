using AutoMapper;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Profiles
{
    public class RegisterPatientProfile:Profile
    {
        public RegisterPatientProfile()
        {
            CreateMap<RegisterPatientDto, ApplicationUser>()
            .ForMember(dst => dst.ProfileImage, opt => opt.Ignore());

            CreateMap<RegisterPatientDto, Patient>()
                .ForMember(dst => dst.ActivityRates, opt => opt.Ignore())
                .ForMember(dst => dst.NoEat, opt => opt.Ignore())
                .ForMember(dst => dst.Goal, opt => opt.Ignore()).ReverseMap();


        }
    }
}
