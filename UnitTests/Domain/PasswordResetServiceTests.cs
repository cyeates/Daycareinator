using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Notifications;
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
    public class PasswordResetServiceTests
    {
        [TestMethod]
        public void ForgotPasswordEmailIsSentWhenUserExists()
        {
            var userRepository = new Mock<IUserRepository>();
            var user = new User { Membership = new Membership { PasswordVerificationToken = "asdkj" } };
            userRepository.Setup(u => u.GetUserByUserName(It.IsAny<string>())).Returns(user);

            var email = new Mock<IEmail>();

            var service = new PasswordResetService(userRepository.Object, email.Object);
            var result = service.SendForgotPasswordEmail("chad");

            email.Verify(e => e.Send(), Times.Once());
            Assert.IsTrue(result.IsValid);
            
        }

        [TestMethod]
        public void ForgotPasswordEmailIsNotSentWhenUserDoesNotExist()
        {
            var userRepository = new Mock<IUserRepository>();
            var user = new User { Membership = new Membership { PasswordVerificationToken = "asdkj" } };
            userRepository.Setup(u => u.GetUserByUserName(It.IsAny<string>())).Returns(It.IsAny<User>());

            var email = new Mock<IEmail>();

            var service = new PasswordResetService(userRepository.Object, email.Object);
            var result = service.SendForgotPasswordEmail("chad");

            email.Verify(e => e.Send(), Times.Never());
            Assert.IsFalse(result.IsValid);
        }

    }
}
