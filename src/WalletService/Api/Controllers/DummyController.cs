using System.Web.Http;

namespace WalletService.Api.Controllers
{
    public class DummyController : ApiController
    {
        [HttpGet]
        [Route("api/dummy/hello")]
        public IHttpActionResult Hello()
        {
            return Ok("Hello from API only project!");
        }
    }
}