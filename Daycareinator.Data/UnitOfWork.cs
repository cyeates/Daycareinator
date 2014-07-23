using Daycareinator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daycareinator.Data.Entities;

namespace Daycareinator.Data
{
    public interface IUnitOfWork
    {
        IRepository<Child> Children { get; }
        IRepository<Client> Clients { get; }
        IRepository<Employee> Employees { get;  }
        IRepository<Record> Records { get; }
        IRepository<TimecardSubmission> TimecardSubmissions { get; }
        IRepository<User> Users { get; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DaycareinatorContext _context;

        private IRepository<Employee> _employees;
        private EfRepository<TimecardSubmission> _timecardSubmissions;
        private EfRepository<Child> _children;
       
        public UnitOfWork(DaycareinatorContext context)
        {
            this._context = context;
        }

        public IRepository<Child> Children
        {
            get
            {
                return _children ?? new EfRepository<Child>(_context);
            }
        }
        public IRepository<Client> Clients
        {
            get
            {
                return new EfRepository<Client>(_context);
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                return _employees ?? new EfRepository<Employee>(_context);
            }
        }

        public IRepository<Record> Records
        {
            get { return new EfRepository<Record>(_context); }
        }
       

        public IRepository<TimecardSubmission> TimecardSubmissions
        {
            get
            {
                return _timecardSubmissions ?? new EfRepository<TimecardSubmission>(_context);
            }
        }

        public IRepository<User> Users
        {
            get 
            {
                return new EfRepository<User>(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }


}
