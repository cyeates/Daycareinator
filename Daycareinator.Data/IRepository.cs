using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Daycareinator.Data
{
    public interface IRepository<T> where T : class
    {
        T FirstOrDefault(Expression<Func<T, bool>> expression);
        T GetById(int id);
        IQueryable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
