
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ErrorRoleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
