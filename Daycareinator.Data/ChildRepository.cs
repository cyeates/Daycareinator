using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data
{
    public interface IChildRepository
    {
        Child GetById(int id);
        IEnumerable<Child> GetChildren(int clientId);
    }

    public class ChildRepository : EfRepository<Child>, IChildRepository
    {
        private DaycareinatorContext _context;
        public ChildRepository(DaycareinatorContext context) : base(context)
        {
            _context = context;
        }

        public Child GetById(int id)
        {
            return base.GetById(id);
        }

        public IEnumerable<Child> GetChildren(int clientId)
        {
            return base.Find(c => c.ClientId == clientId);
        }
    }
}
