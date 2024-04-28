using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Utilities;
using DoAnTapHoaCongNghe.Areas.VNPayAPI.Util;
namespace DoAnTapHoaCongNghe.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _context;
		private readonly PayLib _vnPayservice;
		public CartController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index(Dictionary<string, string> request)
		{
			if (!Functions.IsLogin())
				return RedirectToAction("Index", "login");
			var order = _context.carts.Where(o => o.user_id == Functions._UserID).Select(o => new
			{
				cart = o,
				product = _context.products.Where(p => p.product_id == o.product_id).FirstOrDefault(),
			}).ToList();
			//return Ok(order);
			return View(order);
		}
		[HttpPost]
		public ActionResult AddToCart(int product_id, int quantity, int userid)
		{
			if (!Functions.IsLogin())
				return RedirectToAction("Index", "login");
			var infor = "";
			var product = _context.products.FirstOrDefault(p => p.product_id == product_id);
			if (product == null)
			{
				return NotFound();
			}
			var order = new Models.Cart
			{
				user_id = Functions._UserID,
				product_id = product_id,
				quantity = quantity,
			};
			_context.Add(order);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult Pay()
		{
			int userId = Functions._UserID;
			var listCart = _context.carts.Where(m => m.user_id == userId).ToList();
			decimal totalPay = 0;
			string productstr = "";
			foreach (var item in listCart)
			{
				var product = _context.products.FirstOrDefault(m => m.product_id == item.product_id);
				if (product != null)
				{
					totalPay += product.price;
					productstr += product.product_id + ",";
				}
			}
			var now = DateTime.Now;
			Order newOrder = new Order()
			{
				user_id = userId,
				seller_id = 1,
				total_price = totalPay,
				Time = now,
				order_status = "paid"
			};
			_context.Add(newOrder);
			_context.SaveChanges();
			var orderId = _context.orders.Where(m => m.user_id == newOrder.user_id && m.total_price == newOrder.total_price && m.order_status == newOrder.order_status).OrderByDescending(m=> m.order_id).FirstOrDefault().order_id;
			foreach (var item in listCart)
			{
				_context.Add(new OrderInfo()
				{
					OrderID = orderId,
					ProductID = item.product_id,
					Quantity = item.quantity,
				});
			}
			_context.SaveChanges();
			return Redirect($"/vnpayapi/{Convert.ToInt32(totalPay)}00&{productstr.TrimEnd(',')}&{orderId}");
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
		[Route("/cart/del/{id}")]
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var db = _context.carts.Where(m => m.product_id == id && m.user_id == Functions._UserID).FirstOrDefault();
			if (db == null)
			{
				return NotFound();
			}
			_context.Remove(db);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
