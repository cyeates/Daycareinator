using Daycareinator.Domain;
using Daycareinator.Domain.Notifications;
using Daycareinator.Models.Home;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daycareinator.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactForm contactForm)
        {
            if (ModelState.IsValid && String.IsNullOrEmpty(contactForm.UserName))
            {
                var email = new Email
                {
                    Recipients = new List<string> { ConfigurationManager.AppSettings["ContactFormToAddress"] },
                    Subject = HttpUtility.HtmlEncode(contactForm.Subject),
                    Body = string.Format("<p>Sender's Name: {0}</p><p>{1}</p>", HttpUtility.HtmlEncode(contactForm.Name), HttpUtility.HtmlEncode(contactForm.Message)),
                    From = HttpUtility.HtmlDecode(contactForm.Email)
                };

                try
                {
                    email.Send();
                    return Json(ValidationResult.SuccessMessage("Your message was sent successfully!"));
                }
                catch(Exception ex){
                    return Json(ValidationResult.ErrorMessage("An error occurred while sending your message.  Please call us at (817) 901-4004, or email us at don@waldencpb.com"));   
                }

            }

            return Json(ValidationResult.ErrorMessage("An error occurred while sending your message.  Please call us at (817) 901-4004, or email us at don@waldencpb.com"));  
        }
    }
}
