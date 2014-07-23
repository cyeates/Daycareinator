using Daycareinator.Data;
using Daycareinator.Domain.Services;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Daycareinator.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Daycareinator.App_Start.NinjectWebCommon), "Stop")]

namespace Daycareinator.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Daycareinator.Domain.Membership;
    using Daycareinator.Membership;
    using Daycareinator.Domain.Notifications;
    using Daycareinator.Domain.Validation;
    using System.Security.Principal;
    using Daycareinator.Models.Children;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IChildrenService>().To<ChildrenService>();
            kernel.Bind<IInvitationService>().To<InvitationService>();
            kernel.Bind<IPasswordResetService>().To<PasswordResetService>();
            kernel.Bind<IRecordsService>().To<RecordsService>();
            kernel.Bind<ITimeCardsService>().To<TimecardsService>();
            kernel.Bind<IUsersService>().To<UsersService>();

            kernel.Bind<ICurrentUser>().To<CurrentUser>();
            kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);

            kernel.Bind<IChildModelBuilder>().To<ChildModelBuilder>();

            kernel.Bind<IEmail>().To<Email>();
            kernel.Bind<IMembershipWrapper>().To<SimpleMembershipWrapper>();
            kernel.Bind<ISimpleMembershipInitializer>().To<SimpleMembershipInitializer>();
            

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IChildRepository>().To<ChildRepository>();
            kernel.Bind<IClientRepository>().To<ClientRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<DaycareinatorContext>().To<DaycareinatorContext>();
        }
    }
}
