using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Education;

namespace ServiceHost.Controllers
{
    public class EducationController : SiteBaseController
    {
        #region Fields

        private readonly IEducationService _educationService;



        #endregion


        #region Constructor

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        #endregion


        #region Actions

        public ActionResult Resume()
        {
            return View();
        }

        #endregion


    }
}
