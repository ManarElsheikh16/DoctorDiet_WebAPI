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
using DoctorDiet.Dto;
using DoctorDiet.Repository.UnitOfWork;

namespace DoctorDiet.Services
{
    public class PatientService
    { 
    
        IGenericRepository<Patient, string> _repositry;
    IGenericRepository<Doctor, string> _DoctorRepositry;

    IMapper _mapper;
        NoEatService _NoEatService;
        GoalService _goalService;
        ActivityRateService _ActivityRateService;
        AccountService _accountService;
        IUnitOfWork _unitOfWork;
        public PatientService(IGenericRepository<Patient, string> Repositry, IMapper mapper, NoEatService NoEatService, GoalService goalService, ActivityRateService ActivityRateService, AccountService accountService, IUnitOfWork unitOfWork)
        {
            _repositry = Repositry;
            _mapper = mapper;
            _NoEatService = NoEatService;
            _ActivityRateService = ActivityRateService;
            _goalService = goalService;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
        }
        public Patient GetPatientData(string id)
        {
            Patient patient = _repositry.Get(o => o.Id == id).Include(x => x.ApplicationUser).FirstOrDefault();
            return patient;

        }
        public Patient AddPatient(RegisterPatientDto registerPatientDto)
        {
            Patient patient = _mapper.Map<Patient>(registerPatientDto);
            patient.Id = registerPatientDto.PatientId;
            _accountService.AddPatient(patient);

            foreach (string noEat in registerPatientDto.noEats)
            {
                NoEat noeat = new NoEat
                {

                    PatientId = patient.Id,
                    Name = noEat,

                };
                _NoEatService.AddNoEat(noeat);
            }

            foreach (string activityrate in registerPatientDto.ActivityRates)
            {
                ActivityRate activityRate = new ActivityRate
                {
                    PatientId = patient.Id,
                    Name = activityrate,
                };

                _ActivityRateService.AddActivityRate(activityRate);
            }

            foreach (string goal in registerPatientDto.Goal)
            {
                Goal Goal = new Goal
                {
                    Name = goal,
                    PatientId = patient.Id,
                };
                _goalService.AddGoal(Goal);
            }

            

            return patient;

        }


    public void UpdatePatient(string PatientId, RegisterPatientDto registerPatientDto, params string[] updatedProp)
    {

      Patient patient = _repositry.Get(patient=> patient.Id==PatientId).FirstOrDefault();
      patient= _mapper.Map<Patient>(registerPatientDto);
      _repositry.Update(patient, updatedProp);
      _unitOfWork.SaveChanges();
    }
    public IQueryable<RegisterPatientDto> AllPatients (string DoctorId)
    {
       Doctor doctor=  _DoctorRepositry.Get(doctor=>doctor.Id==DoctorId).FirstOrDefault();
      IQueryable<Patient> patients = (IQueryable<Patient>)doctor.Patients;
     return   _mapper.ProjectTo<RegisterPatientDto>(patients);
 
    }

  }
}
