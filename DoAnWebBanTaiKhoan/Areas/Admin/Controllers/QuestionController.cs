using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnTapHoaCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly DataContext _context;
        public QuestionController(DataContext context)
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
            var tmlist = _context.question.OrderBy(m => m.question_id).ToList();
            return View(tmlist);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.question.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleQuestion = _context.question.Find(id); if (deleQuestion == null)
            {
                return NotFound();
            }
            _context.question.Remove(deleQuestion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.question
                          select new SelectListItem()
                          {
                              Text = m.question,
                              Value = m.question_id.ToString(),
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
        public IActionResult Edit(Question mn)
        {
            if (ModelState.IsValid)
            {
                _context.question.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
    }
}
