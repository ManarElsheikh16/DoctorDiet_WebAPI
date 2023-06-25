using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repositories.Interfaces
{
    public interface IDoctorRepository 
    {
        List<Notes> GetNotes(GetDoctorNotesDTO getdoctorNotesDTO);
        string AddNote(Notes Notes);
    }
}
