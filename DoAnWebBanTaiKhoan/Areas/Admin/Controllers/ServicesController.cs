using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServicesController : Controller
	{
		private readonly DataContext _context;
		public ServicesController(DataContext context)
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
            var tmlist = _context.services.OrderBy(m => m.services_id).ToList();
			return View(tmlist);

		}
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.services.Find(id);
			if (mn == null)
			{
				return NotFound();
			}
			return View(mn);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var delesv = _context.services.Find(id); if (delesv == null)
			{
				return NotFound();
			}
			_context.services.Remove(delesv);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Create()
		{
			var mnList = (from m in _context.services
						  select new SelectListItem()
						  {
							  Text = m.services_name,
							  Value = m.services_id.ToString(),
						  }).ToList();
			mnList.Insert(0, new SelectListItem()
			{
				Text = "----Select----",
				Value = "0"
			});
			ViewBag.mnList = mnList;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Services mn)
		{
			if (ModelState.IsValid)
			{
				_context.services.Add(mn);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(mn);
		}
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var mn = _context.services.Find(id);
			if (mn == null)
			{
				return NotFound();
			}

			var mnList = (from m in _context.services
						  select new SelectListItem()
						  {
							  Text = m.services_name,
							  Value = m.services_id.ToString(),
						  }).ToList();
			mnList.Insert(0, new SelectListItem()
			{
				Text = "----Select----",
				Value = String.Empty
			});
			ViewBag.mnList = mnList;

			return View(mn);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Services mn)
		{
			if (ModelState.IsValid)
			{
				_context.services.Update(mn);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(mn);
		}
	}
}
