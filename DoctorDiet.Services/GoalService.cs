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
    public class GoalService
    {
        IGenericRepository<Goal, int> _GoaRepository;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public GoalService(IGenericRepository<Goal, int> GoalRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _GoaRepository = GoalRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public Goal AddGoal(Goal goal)
        {
/*            Goal goal=_mapper.Map<Goal>(goaldTO);
*/            _GoaRepository.Add(goal);
            _unitOfWork.SaveChanges();

            return goal;

        }
    }
}
