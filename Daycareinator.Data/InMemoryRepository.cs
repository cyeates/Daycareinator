using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T: class
    {
        private List<T> _set; 
        public InMemoryRepository()
        {
            _set = new List<T>();
        }
        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _set.AsQueryable().FirstOrDefault(expression);
        }
        public T GetById(int id)
        {
            return null;
        }

        public IQueryable<T> GetAll()
        {
            return _set.AsQueryable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _set.AsQueryable().Where(expression).ToList();
        }



        public void Add(T entity)
        {
            _set.Add(entity);

        }

        public void AddRange(List<T> entities)
        {
            _set.AddRange(entities);
        }

        public void Update(T entity)
        {
            //var entry = _context.Entry(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    _set.Attach(entity);
            //}

            //entry.State = EntityState.Modified;

        }

        public void Delete(T entity)
        {
            //var entry = _context.Entry(entity);
            //if (entry.State != EntityState.Deleted)
            //{
            //    entry.State = EntityState.Deleted;
            //}
            //else
            //{
            //    _set.Attach(entity);
            //    _set.Remove(entity);
            //}
        }
    }
}
