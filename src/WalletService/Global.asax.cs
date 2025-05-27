using System.Web;
using System.Web.Http;
using WalletService.Api.App_Start;

namespace WalletService.Api
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SwaggerConfig.Register();
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
