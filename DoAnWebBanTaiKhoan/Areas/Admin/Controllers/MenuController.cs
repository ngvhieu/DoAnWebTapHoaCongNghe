using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly DataContext _context;
        public MenuController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.Menus.OrderBy(m => m.MenuID).ToList();           
            return View(mnList);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cc = _context.Menus.Find(id);
            if (cc == null)
            {
                return NotFound();
            }

            return View(cc);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var db = _context.Menus.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            _context.Menus.Remove(db);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var bb = (from x in  _context.Menus
                      select new SelectListItem()
                      {
                          Text = x.MenuName,
                          Value = x.MenuID.ToString(),
                      }).ToList();
            bb.Insert(0, new SelectListItem()
            {
                Text = "Select",
                Value = "0"
            });
            ViewBag.bb = bb;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu cl)
        {
            if (ModelState.IsValid)
            {
                _context.Menus.Add(cl);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cl);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menus.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            var mnList = (from x in _context.Menus
                          select new SelectListItem()
                          {
                              Text = x.MenuName,
                              Value = x.MenuID.ToString(),
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
        public IActionResult Edit(Menu mn)
        {
            if (ModelState.IsValid)
            {
                _context.Menus.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
      
    }
}
