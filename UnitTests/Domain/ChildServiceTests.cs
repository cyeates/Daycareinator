using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    [TestClass]
    public class ChildServiceTests
    {
        [TestMethod]
        public void GetById()
        {
            var repo = new InMemoryRepository<Child>();
            var uow = new Mock<IUnitOfWork>();
            repo.Add(new Child { ChildId = 1 });
            uow.Setup(u => u.Children).Returns(repo);

            var service = new ChildrenService(uow.Object);

            var child = service.GetById(1);

            Assert.AreEqual(1, child.ChildId);
            
        }
    }
}
