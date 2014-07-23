using Daycareinator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Membership
{
    public interface IMembershipWrapper
    {
        bool IsValidInvitationToken(string userName, string token);
        bool IsValidPasswordToken(string userName, string token);
        bool ConfirmAccount(RegisterModel model);
        bool Login(LoginModel model);
        string GeneratePasswordResetToken(string emailAddress);
        bool ResetPassword(string token, string newPassword);



    }
}
