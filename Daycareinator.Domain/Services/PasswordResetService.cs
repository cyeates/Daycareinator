using Daycareinator.Data;
using Daycareinator.Domain;
using Daycareinator.Resources;
using Daycareinator.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IPasswordResetService
    {
        ValidationResult SendForgotPasswordEmail(string emailAddress);
    }
    public class PasswordResetService : IPasswordResetService
    {
        private IUserRepository _userRepository;
        private IEmail _email;
        public PasswordResetService(IUserRepository userRepository, IEmail email)
        {
            _userRepository = userRepository;
            _email = email;
        }

        public ValidationResult SendForgotPasswordEmail(string emailAddress)
        {

            var user = _userRepository.GetUserByUserName(emailAddress);
            if (user != null)
            {
                string token = user.Membership.PasswordVerificationToken;
                string url = String.Format("{0}/Account/PasswordVerification/?userName={1}&token={2}", ConfigurationManager.AppSettings["BaseUrl"], user.EmailAddress, token);
                string link = String.Format("<a href='{0}'>{0}</a>", url);

                var recipients = new List<string> { user.EmailAddress };


                _email.Recipients = recipients;
                _email.Subject = EmailResources.ForgotPasswordSubject;
                _email.Body = String.Format(EmailResources.ForgotPasswordBody, link);


                try
                {
                    _email.Send();
                    return ValidationResult.SuccessMessage(string.Format("A email has been sent to {0} that contains a link where you can reset your password.", user.EmailAddress));
                }
                catch
                {
                    return ValidationResult.ErrorMessage(string.Format("An error occured while sending an email to {0}", user.EmailAddress));
                }


            }

            return ValidationResult.ErrorMessage("We could not find an account with this email address.");


        }
    }
}
