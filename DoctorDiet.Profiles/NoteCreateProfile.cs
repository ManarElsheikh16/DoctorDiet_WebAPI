using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Profiles
{
    public class NoteCreateProfile:Profile
    {
        public NoteCreateProfile()
        {
            CreateMap<NoteCreateDto, Notes>();
            CreateMap<UpdateNoteDto,Notes>();

            CreateMap<DoctorNotesDTO,Notes>().ReverseMap();
        }
    }
}
