using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Tests
{
    [TestFixture]
    public class ClientsServiceTests
    {

        [TestCase("chad")]
        [TestCase("Chad")]
        public void GetClientFromSubdomain(string clientName)
        {
            var client = new Client { Name = "chad" };
            var clientsRepository = new Mock<IRepository<Client>>();
            clientsRepository.Setup(c => c.FirstOrDefault(x => x.Name.ToLower() == clientName.ToLower())).Returns(client);

            var service = new ClientsService(clientsRepository.Object);
            var actual = service.GetBySubDomain("chad.localhost.com");

            Assert.That(actual.Equals(client));

            

        }
    }
}
