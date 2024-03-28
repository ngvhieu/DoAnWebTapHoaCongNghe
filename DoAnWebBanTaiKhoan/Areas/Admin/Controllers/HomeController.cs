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
                return RedirectToAction("Index","Login");
            return View();
        }
        public IActionResult Logout()
        {
            Functions._UserID = 0;
            Functions._UserName = string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            Functions._MessageEmail = string.Empty;
            return RedirectToAction("Index", "Home");
        }
        [Route("/admin/")]
        public IActionResult DBAdmin()
        {
           return Redirect("/admin/home");
        }
    }
}
