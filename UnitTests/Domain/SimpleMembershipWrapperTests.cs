using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Membership;
using Daycareinator.Domain.Membership;
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
    public class SimpleMembershipWrapperTests
    {

        [TestMethod]
        public void TokenMatchesUsersConfirmationToken(){
           
            var user = new User { EmailAddress = "chad.yeates@gmail.com", Membership = new Membership { ConfirmationToken = "myToken" } };
            var repo = new Mock<IUserRepository>();
            repo.Setup(u => u.GetUserByUserName(user.EmailAddress)).Returns(user);
            var wrapper = new SimpleMembershipWrapper(repo.Object, new Mock<ISimpleMembershipInitializer>().Object);

            Assert.IsTrue(wrapper.IsValidInvitationToken(user.EmailAddress, user.Membership.ConfirmationToken));
            Assert.IsFalse(wrapper.IsValidInvitationToken(user.EmailAddress, "invalid token"));
            Assert.IsFalse(wrapper.IsValidInvitationToken("null user", user.Membership.ConfirmationToken));
        }
    }
}
