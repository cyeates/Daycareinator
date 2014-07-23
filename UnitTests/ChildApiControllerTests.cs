using Daycareinator;
using Daycareinator.Controllers;
using Daycareinator.Data.Entities;
using Daycareinator.Domain;
using Daycareinator.Domain.Services;
using Daycareinator.Models.Children;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace UnitTests
{
    [TestClass]
    public class ChildApiControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void ReturnsErrorWhenUserDoesNotHaveAccesstoChild()
        {
            string email = "email@gmail.com";
            var currentUser = new Mock<ICurrentUser>();
            var user = new User { EmailAddress = email, ClientId = 1 };
            currentUser.Setup(c => c.GetCurrentUser).Returns(user);

            var child = new Child { ClientId = 2 };
            var childService = new Mock<IChildrenService>();
            childService.Setup(c => c.GetById(It.IsAny<int>())).Returns(child);

            var builder = new Mock<IChildModelBuilder>();
            var controller = new ChildApiController(childService.Object, builder.Object,  currentUser.Object);

            var result = controller.Get(1);

            



        }
    }
}
