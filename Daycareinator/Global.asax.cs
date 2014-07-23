using Daycareinator.Data;
using Daycareinator.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;


namespace Daycareinator
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Bootstrapper.Initialise();

            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

            
            var context = new DaycareinatorContext();
            context.Database.Initialize(true);
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DaycareinatorContext", "Users", "UserId", "EmailAddress", true);
            }
            
        }
    }
}