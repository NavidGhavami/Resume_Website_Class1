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
            var result =  await _userService.CreateUser(user);

            switch (result)
            {
                case CreateUserResult.Success:
                    ViewBag["Message"] = "ثبت اطلاعات با موفقیت انجام شد";
                    break;
                case CreateUserResult.DuplicateMobile:
                    ViewBag["Message"] = "شماره همراه تکراری می باشد";
                    break;
                case CreateUserResult.Error:
                    ViewBag["Message"] = "خطایی رخ داد. لطفا دوباره تلاش نمایید";
                    break;
            }

            return View();
        }



        #endregion

        #region Edit User

        [HttpGet("edit-user")]
        public async Task<IActionResult> EditUser(long id)
        {
            return View();
        }

        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser(EditUserDto userEdit)
        {
            return View();
        }

        #endregion


        #endregion
    }
}
