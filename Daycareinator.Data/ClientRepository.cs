using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Data
{
    public interface IClientRepository
    {
        Client GetClientByName(string clientName);
    }

    public class ClientRepository : EfRepository<ClientRepository>, IClientRepository
    {
        private DaycareinatorContext _context;
        public ClientRepository(DaycareinatorContext context)
            : base(context)
        {
            _context = context;
        }

        public Client GetClientByName(string clientName)
        {
            return _context.Clients.FirstOrDefault(c => c.Name.ToLower() == clientName.ToLower());
        }
    }
}
