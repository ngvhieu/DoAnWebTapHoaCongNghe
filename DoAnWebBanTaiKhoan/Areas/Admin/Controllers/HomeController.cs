using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Utilities;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!Functions.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                const int adminRoleId = 3; // Khai báo hằng số cho vai trò "Admin"
                if (Functions._Role != adminRoleId)
                {
                    return RedirectToAction("Index", "ErrorRole");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            Functions._UserID = 0;
            Functions._UserName = string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            Functions._MessageEmail = string.Empty;
            Functions._Role = 0;
            return RedirectToAction("Index", "Home");
        }
        [Route("/admin/")]
        public IActionResult DBAdmin()
        {
           return Redirect("/admin/home");
        }
    }
}
