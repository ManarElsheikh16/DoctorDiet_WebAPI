using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.DTO;
using DoctorDiet.Repositories.Interfaces;
using DoctorDiet.Repositories.Repositories;

namespace DoctorDiet.Services
{
    public class DoctorService
    {
        IGenericRepository<Doctor, string> _repositry;
        IDoctorRepository _doctorRepository;
        IMapper _mapper;
        IUnitOfWork _unitOfWork;

        public DoctorService(IDoctorRepository doctorRepository,IGenericRepository<Doctor, string> Repositry, IMapper mapper, IUnitOfWork unitOfWork)
        {

            _repositry = Repositry;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
            
        }

        public DoctorGetDataDto GetDoctorData(string id)
        {

            DoctorGetDataDto doctorDataDTO =
             GetListOfDoctors()
            .FirstOrDefault(d => d.Id == id);
            return doctorDataDTO;

        }
        public Doctor GetById(string Id)
        {
            Doctor doctor = _repositry.Get(d=>d.Id==Id)
            .Include(d=>d.ApplicationUser)
            .FirstOrDefault();
            return doctor;
        }

        public List<Notes> GetDoctorNotes(GetDoctorNotesDTO getPatientNotesDTO)
        {
            List<Notes> doctorNotes = _doctorRepository.GetNotes(getPatientNotesDTO);

            return doctorNotes;
        }

        public void EditDoctorData(DoctorDataDTO doctorData, params string[] properties)
        {
            using var dataStream = new MemoryStream();
            doctorData.ProfileImage.CopyTo(dataStream);

            Doctor doctorMapper = _mapper.Map<Doctor>(doctorData);

            doctorMapper.ApplicationUser.ProfileImage = dataStream.ToArray();

            _repositry.Update(doctorMapper, properties);
            _unitOfWork.SaveChanges();
        }


        public IQueryable<DoctorGetDataDto> GetListOfDoctors()
        {
            IQueryable<Doctor> doctors = _repositry.GetAll();
            IQueryable<DoctorGetDataDto> DoctorsDto = _mapper.ProjectTo<DoctorGetDataDto>(doctors);

            return DoctorsDto;
        }

        public string AddNote(DoctorNotesDTO doctorNotesDto)
        {
            Notes Notes = _mapper.Map<Notes>(doctorNotesDto);
            string Status = _doctorRepository.AddNote(Notes);
            _unitOfWork.SaveChanges();

            return Status;
        }

    }
}
