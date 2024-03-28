using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace DoAnTapHoaCongNghe.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
