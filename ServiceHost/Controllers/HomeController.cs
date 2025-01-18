using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("404-page-not-found")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
