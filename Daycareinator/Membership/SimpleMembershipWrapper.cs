using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Membership;
using Daycareinator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace Daycareinator.Domain.Membership
{
    public class SimpleMembershipWrapper : IMembershipWrapper
    {
        private IUserRepository _userRepository;
        public SimpleMembershipWrapper(IUserRepository userRepository, ISimpleMembershipInitializer membershipInitializer)
        {
            _userRepository = userRepository;
            membershipInitializer.Initialize();
            

        }

        public bool IsValidInvitationToken(string userName, string token)
        {
            var user = _userRepository.GetUserByUserName(userName);
            return user == null ? false : user.Membership.ConfirmationToken.Equals(token);
        }

        public bool IsValidPasswordToken(string userName, string token)
        {
            var user = _userRepository.GetUserByUserName(userName);
            return user == null ? false : user.Membership.PasswordVerificationToken.Equals(token);
        }

        public bool ConfirmAccount(RegisterModel model)
        {
            if (WebSecurity.ConfirmAccount(model.UserName, model.Token))
            {
                var token = WebSecurity.GeneratePasswordResetToken(model.UserName);
                return WebSecurity.ResetPassword(token, model.Password);
                
                
            }

            return false;

        }

        public bool Login(LoginModel model)
        {
            return WebSecurity.Login(model.UserName, model.Password);
        }

        public string GeneratePasswordResetToken(string emailAddress)
        {
            return WebSecurity.GeneratePasswordResetToken(emailAddress);
        }

        public bool ResetPassword(string token, string newPassword)
        {
            return WebSecurity.ResetPassword(token, newPassword);
        }
    }
}
