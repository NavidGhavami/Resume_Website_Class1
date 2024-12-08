using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class HomeController : AdminBaseController
    {
        
        public IActionResult Index()
        {
            return View();
        }

        #region 404_NotFound_Page

        //[HttpGet("404-not-found")]
        //public ActionResult NotFoundPage()
        //{
        //    return View();
        //}
 
        #endregion
    }
}
