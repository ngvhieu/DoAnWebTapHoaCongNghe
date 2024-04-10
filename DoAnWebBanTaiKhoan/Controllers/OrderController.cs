using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnTapHoaCongNghe.Utilities;
namespace DoAnTapHoaCongNghe.Controllers
{
	
	public class OrderController : Controller
	{
		private readonly DataContext _context;
		public OrderController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var order = _context.orders.Select(o => new
			{
				order = o,
				product = _context.products.Where(p => p.product_id == o.product_id).FirstOrDefault(),
			}).ToList();
			//return Ok(order);
			return View(order);
		}

		[HttpPost]
		public ActionResult AddToCart(int product_id, int quantity)
		{
			var product = _context.products.FirstOrDefault(p => p.product_id == product_id);
			if (product == null)
			{
				return NotFound();
			}

			decimal total_price = (decimal)(product.price * quantity);
			var order = new Order
			{
				user_id = 1,
				seller_id = product.seller_id,
				product_id = product_id,
				quantity = quantity,
				total_price = total_price,
				order_status = "Pending",
			};

			// Add order to context
			_context.orders.Add(order);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
			public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var cc = _context.orders.Find(id);
			if (cc == null)
			{
				return NotFound();
			}

			return View(cc);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var db = _context.orders.Find(id);
			if (db == null)
			{
				return NotFound();
			}
			_context.orders.Remove(db);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
