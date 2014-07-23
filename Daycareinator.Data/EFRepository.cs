using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace Daycareinator.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _set;
        public EfRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _set.FirstOrDefault(expression);
        }

        public T GetById(int id)
        {
            return _set.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _set.Where(expression).ToList();
        }



        public void Add(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _set.Add(entity);
            }
            else
            {
                entry.State = EntityState.Added;
            }

        }

        public void Update(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _set.Attach(entity);
            }

            entry.State = EntityState.Modified;

        }

        public void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                _set.Attach(entity);
                _set.Remove(entity);
            }
        }
    }
}
