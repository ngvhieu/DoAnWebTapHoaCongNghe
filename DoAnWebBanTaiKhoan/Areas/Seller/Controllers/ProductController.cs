using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClosedXML.Excel;
using System.IO;

namespace DoAnTapHoaCongNghe.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
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
                const int adminRoleId = 2; 
                if (Functions._Role != adminRoleId)
                {
                    return RedirectToAction("Index", "ErrorRole");
                }
            }
            var tmlist = _context.products.OrderBy(m => m.product_id).ToList();
            return View(tmlist);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}
			if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.products.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}
			var dlprd = _context.products.Find(id); if (dlprd == null)
            {
                return NotFound();
            }
            _context.products.Remove(dlprd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpGet]
		public IActionResult ImportFromExcel()
		{
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}

			return View(); 
		}

	
		[HttpPost]
		[ValidateAntiForgeryToken] 
		public IActionResult ImportFromExcel(IFormFile excelFile)
		{
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}

			if (excelFile == null || excelFile.Length == 0)
			{
				ModelState.AddModelError("excelFile", "Vui lòng chọn một tệp Excel.");
				return View();
			}
			using (var stream = new MemoryStream())
			{
				excelFile.CopyTo(stream); 
				using (var workbook = new XLWorkbook(stream)) 
				{
					var worksheet = workbook.Worksheet(1);
					var rows = worksheet.RowsUsed(); 

					foreach (var row in rows.Skip(1)) 
					{
						var product = new Product
						{
							product_name = row.Cell(1).GetString(),
							product_description = row.Cell(2).GetString(),
							price = Convert.ToDecimal(row.Cell(3).GetString()),
							quantity = 1,
							category = row.Cell(4).GetString(),
							image = row.Cell(5).GetString(),
							type = Convert.ToInt32(row.Cell(6).GetString()),
						};
						_context.products.Add(product);
					}

					_context.SaveChanges(); 
				}
			}

			TempData["SuccessMessage"] = "Import sản phẩm thành công!";
			return RedirectToAction("Index");
		}

		public IActionResult Create()
        {
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}
			var mnList = (from m in _context.products
                          select new SelectListItem()
                          {
                              Text = m.category,
                              Value = m.product_id.ToString(),
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
        public IActionResult Create(Question mn)
        {
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}
			if (ModelState.IsValid)
            {
                _context.question.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
        public IActionResult Edit(int? id)
        {
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}
			if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.question.Find(id);
            if (mn == null)
            {
                return NotFound();
            }

            var mnList = (from m in _context.question
                          select new SelectListItem()
                          {
                              Text = m.question,
                              Value = m.question_id.ToString(),
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
        public IActionResult Edit(Product mn)
        {
			if (!Functions.IsLogin())
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				const int adminRoleId = 2;
				if (Functions._Role != adminRoleId)
				{
					return RedirectToAction("Index", "ErrorRole");
				}
			}
			if (ModelState.IsValid)
            {
                _context.products.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
    }
}
