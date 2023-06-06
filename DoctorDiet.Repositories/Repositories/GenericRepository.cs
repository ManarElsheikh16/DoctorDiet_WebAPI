using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DoctorDiet.Data;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Repository.Repositories
{
    public class GenericRepository<T,Y> : IGenericRepository<T,Y> where T : class, IBaseModel<Y>
    {
        Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public T GetByID(Y id)
        {
            return _context.Set<T>().FirstOrDefault(x => EqualityComparer<Y>.Default.Equals(x.Id, id));
        }

        public T Add(T entity)
        {

            _context.Set<T>().Add(entity);

            return entity;
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Update(T entity, params string[] properties)
        {
            var localEntity = _context.Set<T>().Local.Where(x => EqualityComparer<Y>.Default.Equals(x.Id, entity.Id)).FirstOrDefault();

            EntityEntry entityEntry;

            if (localEntity is null)
            {
                entityEntry = _context.Set<T>().Entry(entity);
            }
            else
            {
                entityEntry =
                    _context.ChangeTracker.Entries<T>()
                    .Where(x => EqualityComparer<Y>.Default.Equals(x.Entity.Id, entity.Id)).FirstOrDefault();
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

        public void Delete(Y id)
        {
            var entity = GetByID(id);
            entity.IsDeleted = true;
        }

        
    }
}
