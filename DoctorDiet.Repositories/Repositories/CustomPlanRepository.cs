using DoctorDiet.Data;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Repositories
{
    public class CustomPlanRepository : ICustomPlanRepository
    {
        Context _context;

        public CustomPlanRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<CustomPlan> GetAll()
        {
            return _context.CustomPlans;
        }

        public IQueryable<CustomPlan> Get(Expression<Func<CustomPlan, bool>> expression)
        {
            return _context.CustomPlans.Where(expression);
        }

        public CustomPlan GetByID(int id)
        {
            return _context.CustomPlans.FirstOrDefault(x => EqualityComparer<int>.Default.Equals(x.Id, id));
        }

        public CustomPlan Add(CustomPlan entity)
        {

            _context.CustomPlans.Add(entity);

            return entity;
        }

        public void Update(CustomPlan entity)
        {
            _context.Update(entity);
        }

        public void Update(CustomPlan entity, params string[] properties)
        {
            var localEntity = _context.Plans.Local.Where(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)).FirstOrDefault();

            EntityEntry entityEntry;

            if (localEntity is null)
            {
                entityEntry = _context.CustomPlans.Entry(entity);
            }
            else
            {
                entityEntry =
                    _context.ChangeTracker.Entries<CustomPlan>()
                    .Where(x => EqualityComparer<int>.Default.Equals(x.Entity.Id, entity.Id)).FirstOrDefault();
            }

            foreach (var property in entityEntry.Properties)
            {
                if (properties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }

        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            entity.IsDeleted = true;
        }

        public List<Day> GetDayList(int planId)
        {
             var days =_context.CustomPlans.FirstOrDefault(x => x.Id == planId)?.Days;

            return days;
        }
    }
}
