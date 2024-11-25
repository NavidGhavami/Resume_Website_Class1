using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Users;
using Resume.Application.Services.Interface.User;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class UserController : AdminBaseController
    {
        #region Fields

        private readonly IUserService _userService;


        #endregion


        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region Users List

        [HttpGet("users-list")]
        public async Task<IActionResult> UserList()
        {
            return View();
        }

        #endregion


        #region Create User

        [HttpGet("create-user")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            return View();
        }



        #endregion

        #endregion
    }
}
