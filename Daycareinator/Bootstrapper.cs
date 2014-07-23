using System.Web.Http;
using Microsoft.Practices.Unity;
using Daycareinator.Domain.Services;
using Daycareinator.Models.Children;
using Daycareinator.Domain.Notifications;
using Daycareinator.Domain.Membership;
using Daycareinator.Membership;
using Daycareinator.Data;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Unity.Mvc4;
using EfficientlyLazy.Crypto;

namespace Daycareinator
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>(); 
            container.RegisterType<IChildrenService,ChildrenService>();
            container.RegisterType<IEmployeesService, EmployeesService>();
            container.RegisterType<IEmployeeTimecardService, EmployeeTimecardService>();
            container.RegisterType<IInvitationService,InvitationService>();
            container.RegisterType<IPasswordResetService,PasswordResetService>();
            container.RegisterType<IRecordsService,RecordsService>();
            container.RegisterType<IUsersService,UsersService>();

            container.RegisterType<IIdentity>(new InjectionFactory(u => HttpContext.Current.User.Identity));
            container.RegisterType<ICurrentUser, CurrentUser>();
           // container.RegisterType<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);
            

            container.RegisterType<IChildModelBuilder,ChildModelBuilder>();

            container.RegisterType<IEmail,Email>();
            container.RegisterType<IMembershipWrapper,SimpleMembershipWrapper>();
            container.RegisterType<ISimpleMembershipInitializer,SimpleMembershipInitializer>();


            container.RegisterType<IUnitOfWork,UnitOfWork>();

            container.RegisterType<IChildRepository,ChildRepository>();
            container.RegisterType<IClientRepository,ClientRepository>();
            container.RegisterType<IUserRepository,UserRepository>();
            container.RegisterType<DaycareinatorContext,DaycareinatorContext>();

            container.RegisterType<ICryptoEngine, RijndaelEngine>(new InjectionConstructor("uJ3ag/q?JV^0^g$28=7U#gRzy+r$e.A,5]K*7q>MwJK~jD!z{~d]~NavmJm*T-c"));

            return container;
        }
    }
}