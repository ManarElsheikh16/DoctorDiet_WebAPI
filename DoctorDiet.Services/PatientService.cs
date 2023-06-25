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
using DoctorDiet.Repositories.Interfaces;
using AutoMapper.QueryableExtensions;
using DoctorDiet.Repository.Repositories;

namespace DoctorDiet.Services
{
    public class PatientService
    { 
    
        IGenericRepository<Patient, string> _Patientrepositry;
        IGenericRepository<DoctorPatientBridge, int> _repositryBridge;
        IGenericRepository<Doctor, string> _DoctorRepositry;
        IGenericRepository<DoctorPatientBridge, int> _doctorPatirentRepository;
        IMapper _mapper;
        NoEatService _NoEatService;
        AccountService _accountService;
        IUnitOfWork _unitOfWork;
        CustomPlanService _customPlanService;
        IPatientRepository _patientRepository;
        public PatientService(IGenericRepository<Patient, string> Repositry,
            IMapper mapper, NoEatService NoEatService, 
            AccountService accountService, IUnitOfWork unitOfWork
            , CustomPlanService customPlanService,
            IPatientRepository patientRepository,
            IGenericRepository<Doctor, string> doctorRepositry,
            IGenericRepository<DoctorPatientBridge, int> doctorPatirentRepository)
        {
            _Patientrepositry = Repositry;
            _mapper = mapper;
            _NoEatService = NoEatService;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _customPlanService = customPlanService;
            _patientRepository = patientRepository;
            _repositryBridge = doctorPatirentRepository;
            _DoctorRepositry = doctorRepositry;
            _doctorPatirentRepository = doctorPatirentRepository;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _Patientrepositry.GetAll();
        }
        public Patient GetPatientData(string id)
        {
           Patient patient = _Patientrepositry.Get(o => o.Id == id).Include(x => x.ApplicationUser).Include(c=>c.CustomPlans).ThenInclude(d=>d.DaysCustomPlan).ThenInclude(m=>m.DayMealCustomPlanBridge).ThenInclude(m=>m.MealCustomPlan).FirstOrDefault();

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

        

            

            return patient;

        }
        public List<List<DayCustomPlan>> GetEveryDayWithMealsOfDay(string patientID)
        {
            List<CustomPlan> customPlans = _customPlanService.GetPlans(d => d.PatientId == patientID).ToList();
            List<DayCustomPlan> day = new List<DayCustomPlan>();
            List<List<DayCustomPlan>> Alldays = new List<List<DayCustomPlan>>();
            foreach (var customPLan in customPlans)
            {
                day = customPLan.DaysCustomPlan;
                Alldays.Add(day);

            }

            return Alldays;

        }

        public List<CustomPlan> GetPatientHistory(string patientID)
        {
            Patient patient = GetPatientData(patientID);
            List<CustomPlan> customPlans = patient.CustomPlans;

            return customPlans;
        }
        
        public List<PatientNotes> GetPateintNotes(GetPatientNotesDTO getPatientNotesDTO)
        {
            List<PatientNotes> patientNotes = _patientRepository.GetNotes(getPatientNotesDTO);

            return patientNotes;
        }

        public string Confirm(SubscribeDto subscribeDto)
        {
            DoctorPatientBridge doctorPatientBridge = _doctorPatirentRepository.
        Get(d => d.DoctorID == subscribeDto.DoctorID && d.PatientID == subscribeDto.PatientId).
        FirstOrDefault();
            doctorPatientBridge.Status = Status.Confirmed;
            _doctorPatirentRepository.Update(doctorPatientBridge, nameof(DoctorPatientBridge.Status));
            _unitOfWork.SaveChanges();

            Patient patient = _Patientrepositry.GetAll().FirstOrDefault(pat => pat.Id == subscribeDto.PatientId);
            _customPlanService.AddCustomPlan(patient);
            _unitOfWork.SaveChanges();

            return (doctorPatientBridge.Status).ToString();
        }

        public string Reject(SubscribeDto subscribeDto) 
        {
            string Status=_patientRepository.Reject(subscribeDto);
            _unitOfWork.SaveChanges();

            return Status;
        }

        public PatientDTO Subscription(SubscribeDto subscribeDto)
        {
           PatientDTO patientDTO= _patientRepository.Subscription(subscribeDto);
            _unitOfWork.SaveChanges();

            return patientDTO;

        }

    public PatientDTO GetPatientDTO(string PatientId)
    {
      PatientDTO patientDTO = _patientRepository.GetPatientDTO(PatientId);
     
      Patient p = _patientRepository.GetByID(PatientId);
      patientDTO.CustomPlans = _mapper.Map<List<CustomPlanDTO>>(p.CustomPlans);
      _unitOfWork.SaveChanges();

      return patientDTO;

    }
    public UserDataDTO GetPatientDataDTO(string id)
        {
            Patient patient = GetPatientData(id);
            var patientdto = _mapper.Map<UserDataDTO>(patient);
            return patientdto;

        }

        public IEnumerable<PatientDTO> GetPatientsByDoctorIdWithStatusConfirmed(string doctorID)
        {

            var DoctorPatientsBridge = _repositryBridge
            .Get(p => p.DoctorID == doctorID && p.Status.Equals(Status.Confirmed))
            .Include(b=>b.Patient)
            .ThenInclude(p=>p.ApplicationUser);

            var patientDTOs = DoctorPatientsBridge.ProjectTo<PatientDTO>(_mapper.ConfigurationProvider);

            return patientDTOs;
        }

        public IEnumerable<PatientDTO> GetPatientsByDoctorIdWithStatusWaiting(string doctorID)
        {

            var DoctorPatientsBridge = _repositryBridge.Get(p => p.DoctorID == doctorID && p.Status.Equals(Status.Waiting));

            var patientDTOs = _mapper.ProjectTo<PatientDTO>(DoctorPatientsBridge);

            return patientDTOs;
        }
        public void UpdatePatient(string PatientId, RegisterPatientDto registerPatientDto, params string[] updatedProp)
        {

            Patient patient = _Patientrepositry.Get(patient => patient.Id == PatientId).FirstOrDefault();
            patient = _mapper.Map<Patient>(registerPatientDto);
            _Patientrepositry.Update(patient, updatedProp);
            _unitOfWork.SaveChanges();
        }

    public string AddNote(PatientNotesDTO patientNotesDto)
    {
      PatientNotes patientNotes = _mapper.Map<PatientNotes>(patientNotesDto);
      string Status = _patientRepository.AddNote(patientNotes);
      _unitOfWork.SaveChanges();

      return Status;
    }

  }
}
