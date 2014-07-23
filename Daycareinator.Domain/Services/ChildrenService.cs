using Daycareinator.Data;
using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IChildrenService
    {
        Child GetById(int id);
        IEnumerable<Child> GetChildren(int clientId);
    }
    public class ChildrenService : IChildrenService
    {
        private IChildRepository _childRepository;
        private IUnitOfWork _uow;
        public ChildrenService(IUnitOfWork uow)
        {
            //_childRepository = childRepository;
            _uow = uow;
        }

        public Child GetById(int id)
        {
            //return _childRepository.GetById(id);
            return _uow.Children.FirstOrDefault(c => c.ChildId == id);
        }

        public IEnumerable<Child> GetChildren(int clientId)
        {
            return _uow.Children.Find(c => c.ClientId == clientId);
        }

        
    }
}
