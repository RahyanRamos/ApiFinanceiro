using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/")]
    [ApiController]
    public class PrincipalController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { api = "ApiFinanceiro", status = "up" });
        }
    }
}
