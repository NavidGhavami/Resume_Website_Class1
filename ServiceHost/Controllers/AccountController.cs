using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Users;
using Resume.Application.Services.Interface.User;

namespace ServiceHost.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Fields

        private readonly IUserService _userService;



        #endregion

        #region Constructor

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions


        #region Login

        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Administration" });
            }

            return View();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto command)
        {

	        if (!ModelState.IsValid)
	        {
		        return View();
	        }

            var result = await _userService.LoginUser(command);

            switch (result)
            {
                case LoginUserResult.Success:
                    var user = await _userService.GetUserByMobile(command.Mobile);

                    #region Login

                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{user.Fullname}"),
                        new Claim(ClaimTypes.MobilePhone, user.Mobile)
                    };

                    var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    #endregion

                    TempData[SuccessMessage] = $"{user.Fullname} عزیز، خوش آمدید";
                    break;
                case LoginUserResult.Error:
                    TempData[ErrorMessage] = "در انجام عملیات خطایی رخ داد . لطفا دوباره تلاش نمایید";
                    break;
                case LoginUserResult.NotUserFound:
                    TempData[ErrorMessage] = "کاربری با مشخصات فوق یافت نشد.";
                    break;
            }

            return View();
        }

        #endregion

        #region Logout

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        #endregion


        #endregion
    }
}
