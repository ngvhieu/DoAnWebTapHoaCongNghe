using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace DoAnTapHoaCongNghe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public ActionResult Index()
        //{
        //    var menus = from(item in _context.Menus select ).ToList();
        //    ViewBag.CurrentUrl = Request.Url.AbsolutePath; // Lấy URL hiện tại và gán vào ViewBag
        //    return View(menus);
        //}
        [Route("/test")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
