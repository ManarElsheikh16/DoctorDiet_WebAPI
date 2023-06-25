using DoctorDiet.Dto;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Interfaces
{
    public interface ICustomPlanRepository : IGenericRepository<CustomPlan, int>
    {
        List<DayCustomPlan> GetDayList(int id);
        CustomDayDTO GetDayCustomPlan(int CustomPlanid);
        CustomPlan AddCustomPlan(CustomPlan entity);
        DayCustomPlan AddDayCustomPlan(DayCustomPlan dayCustomPlan);
        MealCustomPlan AddMealCustomPlan(MealCustomPlan mealCustomPlan);
        DayMealCustomPlanBridge AddDayMealCustomPlanBridge(DayMealCustomPlanBridge DayMealCustomPlan);
        MealCustomPlan UpdateMealCustomPlan(MealCustomPlan mealCustomPlan, params string[] properties);

    }
}
