using Daycareinator.Domain.Notifications;
using Daycareinator.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IInvitationService 
    {
        void SendInvitation(string emailAddress, string token);
    }

    public class InvitationService : IInvitationService
    {
        public void SendInvitation(string emailAddress, string token)
        {

            var recipients = new List<string>{emailAddress};
            string url = String.Format("{0}/Account/Confirm/?userName={1}&token={2}", ConfigurationManager.AppSettings["BaseUrl"], emailAddress, token);
            string link = String.Format("<a href='{0}'>{0}</a>", url);
            var email = new Email
            {
                Recipients = recipients,
                Subject = EmailResources.InvitationEmailSubject,
                Body = String.Format(EmailResources.InvitationEmailBody, link)
            };

            email.Send();
        }
    }
}
