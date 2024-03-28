using DoAnTapHoaCongNghe.Areas.Admin.Models;
using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/admin/login")]
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // Action mặc định để hiển thị trang đăng nhập
        //[HttpGet]
        public IActionResult Index()
        {
            //// Lưu trữ URL tham chiếu trong TempData
            //TempData["Referrer"] = Request.Headers["Referer"].ToString();
            return View();
        }

        // Action xử lý đăng nhập khi nhận yêu cầu POST từ form đăng nhập
        [HttpPost]
        //[Route("Admin")]
        public IActionResult Index( admin user)
        {
            if (user == null)
            {
                return NotFound();
            }

            // Mã hóa mật khẩu trước khi kiểm tra
            string hashedPassword = Functions.MD5Password(user.password);
            user.password = hashedPassword;

            // Kiểm tra sự tồn tại của người dùng với email và mật khẩu đã mã hóa
            var check = _context.admins
                .Where(m => m.email == user.email && m.password == hashedPassword)
                .FirstOrDefault();

            if (check == null)
            {
                Functions._Message = "Sai email hoac pass";
                return RedirectToAction("Index", "Login");
            }

            Functions._Message = string.Empty;

            Functions._UserID = check.admin_id;
            Functions._UserName = string.IsNullOrEmpty(check.username) ? string.Empty : check.username;
            Functions._Email = string.IsNullOrEmpty(check.email) ? string.Empty : check.email;

            // Nếu không có URL tham chiếu hoặc URL không hợp lệ, chuyển hướng đến trang mặc định
            return RedirectToAction("Index", "Home");
        }
    }
}
