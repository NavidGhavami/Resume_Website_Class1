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
        public async Task<IActionResult> UserList(FilterUserDto filter, string fullname, string mobile)
        {
            filter.Fullname = fullname;
            filter.Mobile = mobile;

            var user = await _userService.GetAllUsers(filter);
            return View(user);
        }

        #endregion

        #region Create User

        [HttpGet("create-user")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost("create-user"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.CreateUser(user);

            switch (result)
            {
                case CreateUserResult.Success:
                    return RedirectToAction("UserList", "User", new { area = "Administration" });
                case CreateUserResult.DuplicateMobile:
                    TempData["DuplicateMobile"] = "شماره همراه وارد شده تکراری می باشد";
                    break;
                case CreateUserResult.Error:
                    //return RedirectToAction("NotFoundPage", "Home", new {area = "Administration"});
                    return NotFound();
                    break;
            }

            return View(user);
        }



        #endregion

        #region Edit User

        [HttpGet("edit-user/{id}")]
        public async Task<IActionResult> EditUser(long id)
        {
            var user = await _userService.GetForEdit(id);

            ViewBag.Fullname = user.Fullname;

            if (user.Id == 0)
            {
                return RedirectToAction("NotFound","Home");
            }

            return View(user);
        }

        [HttpPost("edit-user/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserDto userEdit, IFormFile avatar)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var result = await _userService.EditUser(userEdit, avatar);

            switch (result)
            {
                case EditUserResult.Success:
                    return RedirectToAction("UserList", "User", new { area = "Administration" });
                    break;
                case EditUserResult.UserNotFound:
                    return NotFound();
                break;
                case EditUserResult.Error:
                    return NotFound();
            }


            return View(userEdit);
        }

        #endregion

        #region Block and UnBlock User

        [HttpGet("block-user/{id}")]
        public async Task<IActionResult> BlockUser(long id)
        {
            var user = await _userService.BlockUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserList", "User", new { area = "Administration" });
        }

        [HttpGet("unblock-user/{id}")]
        public async Task<IActionResult> UnBlockUser(long id)
        {
            var user = await _userService.UnBlockUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserList", "User", new { area = "Administration" });
        }


        #endregion


        #endregion
    }
}
