using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Interfaces
{
    public interface ICustomPlanRepository:IGenericRepository<CustomPlan, int>
    {
        List<Day> GetDayList(int id);
    }
}
