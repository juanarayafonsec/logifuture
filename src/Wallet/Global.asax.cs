using System;
using System.Web;
using System.Web.Http;
using Unity.Lifetime;
using Unity;
using WalletService.Api.App_Start;
using WalletService.Business.Services;
using WalletService.Business.Interfaces;
using WalletService.Data.Repository;
using WalletService.Data.Context;
using WalletService.Api.DependencyInjection;

namespace WalletService.Api
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SwaggerConfig.Register();

            RegisterDependencies();
            InitializeDatabase();
        }

        private void RegisterDependencies()
        {
            var container = new UnityContainer();

            container.RegisterType<IWalletRepository, WalletRepository>();
            container.RegisterType<ITransactionService, TransactionService>();
            container.RegisterType<WalletDbContext, WalletDbContext>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var context = new WalletDbContext())
                {
                    context.Database.Initialize(force: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize or migrate the WalletDb database. " +
                                    "Check if the SQL Server container is running and the connection string is correct.",
                                    ex);
            }
        }

        protected void Application_BeginRequest()
        {
            if (HttpContext.Current.Request.Url.AbsolutePath == "/")
            {
                HttpContext.Current.Response.Redirect("~/swagger");
            }
        }
    }
}
