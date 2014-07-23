using Daycareinator.Data;
using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IClientsService
    {
        Client GetBySubDomain(string host);
        
    }
    
    public class ClientsService : IClientsService
    {
       
        private IClientRepository _repository;
        public ClientsService(IClientRepository repository)
        {
            _repository = repository;
        }

        public Client GetBySubDomain(string host)
        {
            string subdomain = string.Empty;
            if (!string.IsNullOrEmpty(host))
            {
                var parts = host.Split('.');
                if (parts.Length > 2)
                {
                    subdomain = parts[0];
                }
            }

            return _repository.GetClientByName(subdomain);
        }

    }
}
