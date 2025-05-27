using System.Web.Http;
using Swashbuckle.Application;

namespace WalletService.Api.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Your API");
                })
                .EnableSwaggerUi();
        }
    }
}