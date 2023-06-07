using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Services
{
    public class ActivityRateService
    {
        IGenericRepository<ActivityRate, int> _activityRateRepository;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public ActivityRateService(IGenericRepository<ActivityRate, int> activityRateService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _activityRateRepository = activityRateService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ActivityRate AddActivityRate(ActivityRate activityRate)
        {
/*            ActivityRate activityRate = _mapper.Map<ActivityRate>(activityRateDto);*/
            _activityRateRepository.Add(activityRate);
            _unitOfWork.SaveChanges();

            return activityRate;

        }
    }
}
