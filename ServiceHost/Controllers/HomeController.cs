using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class HomeController : AdminBaseController
    {
        [Area("Administration")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
