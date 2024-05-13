using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoAnTapHoaCongNghe.Utilities;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class HeroSectionController : Controller
    {
		private readonly DataContext _context;
		public HeroSectionController(DataContext context)
		{
			_context = context;
		}
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
            var mnList = _context.HeroSections.OrderBy(m => m.Id).ToList();
			return View(mnList);
		}
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (!Functions.IsLogin())
				return RedirectToAction("Index", "Login");
			if (Functions._Role != 3)
			{
				return RedirectToAction("Index", "Home");
			}
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.HeroSections.Find(id);
			if (mn == null)
			{
				return NotFound();
			}
			var mnList = (from x in _context.HeroSections
						  select new SelectListItem()
						  {
							  Text = x.Topic,
							  Value = x.Id.ToString(),
						  }).ToList();
			mnList.Insert(0, new SelectListItem()
			{
				Text = "Select",
				Value = "0"
			});
			ViewBag.mnList = mnList;
			return View(mn);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(HeroSection mn)
		{
			if (ModelState.IsValid)
			{
				_context.HeroSections.Update(mn);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(mn);
		}
	}
}
