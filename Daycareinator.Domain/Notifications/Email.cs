using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Notifications
{
    public interface IEmail
    {
        string From { get; set; }
        List<string> Recipients { get; set; }
        string Subject { get; set; }
        string Body { get; set; }

        bool Send();
    }
    public class Email : IEmail
    {
        private readonly string _smtpHost;
        private readonly int _port;

        public string From { get; set; }
        public List<string> Recipients { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }

        public Email()
        {
            _smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            _port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            From = ConfigurationManager.AppSettings["EmailFromAddress"];
            Attachments = new List<Attachment>();
            Body = String.Empty;
            CC = new List<string>();
        }
        public bool Send()
        {
            MailMessage mail = new MailMessage
                                    {
                                        From = new MailAddress(this.From),
                                        Subject = Subject,
                                        Body = Body
                                    };

            foreach (var recipient in Recipients)
            {
                mail.To.Add(new MailAddress(recipient));
            }

            foreach (var cc in CC)
            {
                mail.CC.Add(new MailAddress(cc));
            }

            foreach (var attachment in Attachments)
            {
                mail.Attachments.Add(attachment);
            }
            

            var alternameView = AlternateView.CreateAlternateViewFromString(Body, new ContentType("text/html"));
            mail.AlternateViews.Add(alternameView);

            var smtpClient = new SmtpClient(_smtpHost, _port);
            smtpClient.Credentials = new NetworkCredential("support@waldenbookkeeping.com", "tfESf9EDJaS7");

            smtpClient.Send(mail);


            return true;
        }
    }
}
