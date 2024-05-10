using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			var mnList = _context.HeroSections.OrderBy(m => m.Id).ToList();
			return View(mnList);
		}
		[HttpGet]
		public IActionResult Edit(int? id)
		{
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
