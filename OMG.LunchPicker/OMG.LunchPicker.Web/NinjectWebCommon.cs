using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Web.Common;
using Ninject.Web.WebApi.FilterBindingSyntax;
using System.Web.Http;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using System.Configuration;
using Repository.Pattern.UnitOfWork;
using OMG.LunchPicker.Repository;
using OMG.LunchPicker.Services;
using OMG.LunchPicker.Logging;
using Ninject.Web.WebApi.Filter;
using System.Web.Http.Validation;
using OMG.LunchPicker.Objects.Domain.Validators;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OMG.LunchPicker.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OMG.LunchPicker.Web.NinjectWebCommon), "Stop")]

namespace OMG.LunchPicker.Web
{
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

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //<!-- Context and Uow bindings-->
            kernel.Bind<IDataContextAsync>()
                .To<DataContext>()
                .InSingletonScope()
                .WithConstructorArgument("nameOrConnectionString",
                    context => ConfigurationManager.ConnectionStrings["OMGLunchPickerContext"].ConnectionString);

            kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope().Intercept().With<LoggingInterceptor>();

            //kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(config.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));

            // <!-- Repository bindings-->
            kernel.Bind<IDefaultRepository>().To<DefaultRepository>().InRequestScope().Intercept().With<LoggingInterceptor>();
            kernel.Bind<IRestaurantRepository>().To<RestaurantRepository>().InRequestScope().Intercept().With<LoggingInterceptor>();
            kernel.Bind<ICuisineRepository>().To<CuisineRepository>().InRequestScope().Intercept().With<LoggingInterceptor>();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope().Intercept().With<LoggingInterceptor>();

            // <!-- Service bindings-->
            kernel.Bind<IRestaurantService>().To<RestaurantService>().InRequestScope().Intercept().With<LoggingInterceptor>();
            kernel.Bind<ICuisineService>().To<CuisineService>().InRequestScope().Intercept().With<LoggingInterceptor>();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope().Intercept().With<LoggingInterceptor>();

            // <!-- service validator bindings-->
            kernel.Bind<IUserValidator>().To<UserValidator>().InRequestScope();
            kernel.Bind<IRestaurantValidator>().To<RestaurantValidator>().InRequestScope();
            kernel.Bind<ICuisineValidator>().To<CuisineValidator>().InRequestScope();

            //kernel.Bind<AuthContext>().To<AuthContext>().InRequestScope();
            //kernel.Bind<IAuthRepository>().To<AuthRepository>().InRequestScope();
        }
    }
}
