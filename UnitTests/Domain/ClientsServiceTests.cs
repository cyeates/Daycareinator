using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Tests
{
    [TestClass]
    public class ClientsServiceTests
    {

       
        [TestMethod]
        public void GetClientFromSubdomain()
        {
            var clientName = "chad";
            var client = new Client { Name = clientName };
            var clientsRepository = new Mock<IClientRepository>();
            clientsRepository.Setup(c => c.GetClientByName(clientName)).Returns(client);

            var service = new ClientsService(clientsRepository.Object);
            var actual = service.GetBySubDomain("chad.localhost.com");

            Assert.AreEqual(client.Name, actual.Name);

            

        }
    }
}
