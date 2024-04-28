using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAnTapHoaCongNghe.Controllers
{
    public class UserController : Controller
    {
		private readonly DataContext _dataContext;
		public UserController (DataContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index() 
		{
			var infoUser = (from m in _dataContext.users.Where(m=> m.user_id == Functions._UserID)
							select m).FirstOrDefault();
			if (!Functions.IsLogin())
				return RedirectToAction("Index", "login");
			//return Ok(order);
			return View(infoUser);
		}
    }
}
