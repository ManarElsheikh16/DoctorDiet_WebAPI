using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.DTO;
using AutoMapper;

namespace DoctorDiet.Services
{
    public class DoctorService
    {
        IGenericRepository<Doctor, string> _repositry;
        IMapper _mapper;

        public DoctorService(IGenericRepository<Doctor, string> Repositry, IMapper mapper)
        {

            _repositry = Repositry;
            _mapper = mapper;

        }

        public Doctor GetDoctorData(string id)
        {

            Doctor doctor = _repositry.Get(o => o.Id == id).Include(x => x.ApplicationUser).FirstOrDefault();
            return doctor;

        }
        public IQueryable<RegisterDoctorDto> GetListOfDoctors()
        {
          IQueryable<Doctor> doctors = _repositry.GetAll();
            IQueryable<RegisterDoctorDto> DoctorsDto=  _mapper.ProjectTo<RegisterDoctorDto>(doctors);

            return DoctorsDto;
        }
    }
}
