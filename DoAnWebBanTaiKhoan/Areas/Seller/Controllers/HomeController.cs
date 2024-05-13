using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Areas.Seller.Controllers
{
    [Area("Seller")]
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
                const int sellerRoleId = 2;
                if (Functions._Role != sellerRoleId)
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
        [Route("/seller/")]
        public IActionResult DBAdmin()
        {
            return Redirect("/seller/home");
        }
    }
}

