using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Daycareinator.Filters
{
    public class AuthorizeEmployeeAccess : AuthorizeAttribute
    {
        
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            
            base.OnAuthorization(actionContext);
        }
    }
}