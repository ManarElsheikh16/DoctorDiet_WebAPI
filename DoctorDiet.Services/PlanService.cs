using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Services
{
    public class PlanService
    {
        private readonly IPlanRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IPlanRepository repository,IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork= unitOfWork;

        }

        public IQueryable<Plan> GetAllPlans()
        {
            return _repository.GetAll();
            _unitOfWork.SaveChanges();
        }

        public IQueryable<Plan> GetPlans(Expression<Func<Plan, bool>> expression)
        {
            return _repository.Get(expression);
            _unitOfWork.SaveChanges();
        }

        public Plan GetPlanById(int id)
        {
            return _repository.GetByID(id);
            _unitOfWork.SaveChanges();
        }

        public Plan AddPlan(Plan plan)
        {
            return _repository.Add(plan);
            _unitOfWork.SaveChanges();
        }

        public void UpdatePlan(Plan plan)
        {
            _repository.Update(plan);
            _unitOfWork.SaveChanges();
        }

        public void DeletePlan(int id)
        {
            _repository.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
