using DoAnTapHoaCongNghe.Areas.Admin.Models;
using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RegisterAdminController : Controller
    {
        private readonly DataContext _context;
        public RegisterAdminController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(admin user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var check = _context.admins.Where(m => m.email == user.email).FirstOrDefault();
            if (check != null)
            {
                Functions._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "RegisterAdmin");
            }
            Functions._MessageEmail = string.Empty;
            user.password = Functions.MD5Password(user.password);
            _context.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "LoginAdmin");
        }
    }
}
