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
using DoctorDiet.Data;
using AutoMapper;
using DoctorDiet.Dto;

namespace DoctorDiet.Services
{
    public class CustomPlanService
    {
        ICustomPlanRepository _CustomPlanRepository;
        IUnitOfWork _unitOfWork;
        IPlanRepository _PlanRepository;
        IMapper _mapper;

        public CustomPlanService(IMapper mapper, ICustomPlanRepository repository, IUnitOfWork unitOfWork, IPlanRepository PlanRepository)
        {
            _CustomPlanRepository = repository;
            _unitOfWork = unitOfWork;
            _PlanRepository = PlanRepository;
            _mapper = mapper;
        }

        public IQueryable<CustomPlan> GetAllPlans()
        {
            return _CustomPlanRepository.GetAll();

        }

        public IQueryable<CustomPlan> GetPlans(Expression<Func<CustomPlan, bool>> expression)
        {
            return _CustomPlanRepository.Get(expression);

        }

        public CustomPlan GetPlanById(int id)
        {
            return _CustomPlanRepository.GetByID(id);

        }

        public List<DayCustomPlan> GetDaysById(int id)
        {
            return _CustomPlanRepository.GetDayList(id);

        }

        public CustomPlan AddCustomPlan(Patient CurrentPatient)
        {

            Plan plan = _PlanRepository.GetAll().Include(d => d.Days).ThenInclude(m => m.DayMeal).ThenInclude(M => M.Meal).FirstOrDefault(p => p.CaloriesFrom <= CurrentPatient.MinCalories && p.CaloriesTo >= CurrentPatient.MaxCalories);

            CustomPlan customPlan = new CustomPlan();
            customPlan = _mapper.Map<CustomPlan>(plan);

            customPlan.DateFrom = DateTime.Now;
            customPlan.DateTo = DateTime.Now.AddDays(plan.Duration);
            customPlan.PatientId = CurrentPatient.Id;


            _CustomPlanRepository.AddCustomPlan(customPlan);
            _unitOfWork.SaveChanges();

            for (int i = 0; i < plan.Duration / plan.Days.Count; i++)
            {
                foreach (Day Day in plan.Days)
                {

                    DayCustomPlan dayCustomPlan = new DayCustomPlan();
                    dayCustomPlan.CustomPlanId = customPlan.Id;
                    _CustomPlanRepository.AddDayCustomPlan(dayCustomPlan);
                    _unitOfWork.SaveChanges();

                    foreach (DayMealBridge dayMeal in Day.DayMeal)
                    {

                        MealCustomPlan mealCustomPlan = _mapper.Map<MealCustomPlan>(dayMeal.Meal);

                        _CustomPlanRepository.AddMealCustomPlan(mealCustomPlan);
                        _unitOfWork.SaveChanges();


                        DayMealCustomPlanBridge dayMealBridge = new DayMealCustomPlanBridge()
                        {
                            DayId = dayCustomPlan.Id,
                            MealId = mealCustomPlan.Id,
                        };
                        _CustomPlanRepository.AddDayMealCustomPlanBridge(dayMealBridge);
                        _unitOfWork.SaveChanges();
                    }



                }
            }

            return customPlan;

        }

        public void UpdatePlan(CustomPlan plan)
        {
            _CustomPlanRepository.Update(plan);
            _unitOfWork.SaveChanges();
        }

        public void UpdatePlanProperties(CustomPlan plan, params string[] properties)
        {
            _CustomPlanRepository.Update(plan, properties);
            _unitOfWork.SaveChanges();
        }

        public void DeletePlan(int id)
        {
            _CustomPlanRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public CustomDayDTO GetDayCustomPlan(int id)
        {
            CustomDayDTO customDayDTO = _CustomPlanRepository.GetDayCustomPlan(id);
            return customDayDTO;
        }

        public MealCustomPlan UpdateMealInCustomPlan(UpdateMealDTO UodateMealDTO, params string[] properties)
        {

            using var dataStream = new MemoryStream();
            UodateMealDTO.Image.CopyTo(dataStream);

            MealCustomPlan mealCustomPlan = _mapper.Map<MealCustomPlan>(UodateMealDTO);

            mealCustomPlan.Image = dataStream.ToArray();


            _CustomPlanRepository.UpdateMealCustomPlan(mealCustomPlan, properties);
            _unitOfWork.SaveChanges();

            return mealCustomPlan;
        }
    }
}
