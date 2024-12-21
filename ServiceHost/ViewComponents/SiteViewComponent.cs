using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;

namespace ServiceHost.ViewComponents
{
	#region Site Header

	public class SiteHeaderViewComponent : ViewComponent
	{
		private readonly IUserService _userService;

		public SiteHeaderViewComponent(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _userService.GetUserDetail();
			return View("SiteHeader",user);
		}
	}

	#endregion

	#region Site Footer

	public class SiteFooterViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("SiteFooter");
		}
	}

	#endregion
}
