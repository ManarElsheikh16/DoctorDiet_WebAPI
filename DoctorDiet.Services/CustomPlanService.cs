using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using System.Linq.Expressions;
using DoctorDiet.Repository.UnitOfWork;

namespace DoctorDiet.Services
{
    public class CustomPlanService
    {
        private ICustomPlanRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomPlanService(ICustomPlanRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<CustomPlan> GetAllPlans()
        {
            return _repository.GetAll();
            _unitOfWork.SaveChanges();
        }

        public IQueryable<CustomPlan> GetPlans(Expression<Func<CustomPlan, bool>> expression)
        {
            return _repository.Get(expression);
            _unitOfWork.SaveChanges();
        }

        public CustomPlan GetPlanById(int id)
        {
            return _repository.GetByID(id);
            _unitOfWork.SaveChanges();
        }

        public List<Day> GetDaysById(int id)
        {
            return _repository.GetDayList(id);
            _unitOfWork.SaveChanges();
        }

        public CustomPlan AddPlan(CustomPlan plan)
        {
            return _repository.Add(plan);
            _unitOfWork.SaveChanges();
        }

        public void UpdatePlan(CustomPlan plan)
        {
            _repository.Update(plan);
            _unitOfWork.SaveChanges();
        }

        public void UpdatePlanProperties(CustomPlan plan, params string[] properties)
        {
            _repository.Update(plan, properties);
            _unitOfWork.SaveChanges();
        }

        public void DeletePlan(int id)
        {
            _repository.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
