﻿using DoAnTapHoaCongNghe.Areas.Admin.Models;
using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Controllers
{
    [Route("~/register")]
    public class RegisterUserController : Controller
	{
		private readonly DataContext _context;
		public RegisterUserController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(User user)
		{
			if (user == null)
			{
				return NotFound();
			}
			var check = _context.users.Where(m => m.email == user.email).FirstOrDefault();
			if (check != null)
			{
				Functions._MessageEmail = "Duplicate Email!";
				return RedirectToAction("Index", "Register");
			}
			Functions._MessageEmail = string.Empty;
			user.password = Functions.MD5Password(user.password);
			_context.Add(user);
			_context.SaveChanges();

			return RedirectToAction("Index", "Login");
		}
	}
}
