using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Profiles
{
    public class ActivityRateProfile:Profile
    {
        public ActivityRateProfile()
        {
            CreateMap<ActivityRateDTO,ActivityRate>();
        }
    }
}
