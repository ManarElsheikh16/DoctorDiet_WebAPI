using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Profiles
{
  public class PlanProfile : Profile
  {
    public PlanProfile()
    {
      CreateMap<AddPlanDTO, Plan>()
       .ForMember(dst => dst.Allergics, opt => opt.Ignore())
       .ForMember(dst => dst.Days, opt => opt.Ignore());

     
    }
  }
}
