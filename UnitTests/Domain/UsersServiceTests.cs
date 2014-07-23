using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    [TestClass]
    public class UsersServiceTests
    {
        [TestMethod]
        public void GetUsersInAccountByCurrentUser()
        {
            var user = new User{EmailAddress = "chad.yeates@gmail.com", ClientId = 1};
            var anotherUser = new User{EmailAddress = "apu@kwik-e-mart.com", ClientId= 1};
            
            var users = new List<User> {user, anotherUser };

            var userRepository = new InMemoryRepository<User>();
            userRepository.Add(user);
            userRepository.Add(anotherUser);

            var uow = new Mock<IUnitOfWork>();
            uow.Setup(u => u.Users).Returns(userRepository);

            var service = new UsersService(uow.Object);
            ICollection actual = (ICollection)service.GetUsers(user.EmailAddress);

            CollectionAssert.Contains(actual, user);
            CollectionAssert.Contains(actual, anotherUser);
        }
    }
}
