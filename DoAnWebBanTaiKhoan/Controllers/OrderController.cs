using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using DoAnTapHoaCongNghe.Areas.VNPayAPI.Util;
using DoAnTapHoaCongNghe.ViewModels;
namespace DoAnTapHoaCongNghe.Controllers
{
	public class OrderController : Controller
	{
		private readonly DataContext _context;
		private readonly PayLib _vnPayservice;
		public OrderController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			// Lấy danh sách đơn hàng của người dùng
			var successfulOrders = (from order in _context.orders
									join orderInfo in _context.orderinfos on order.order_id equals orderInfo.OrderID
									join product in _context.products on orderInfo.ProductID equals product.product_id
									join seller in _context.sellers on product.seller_id equals seller.seller_id
									//where order.user_id == Functions._UserID && order.order_status == "paid"
									select new OrderViewModel
									{
										OrderID = order.order_id,
										ProductName = product.product_name,
										ProductPrice = product.price,
										ShopName = seller.shop_name,
										ProductID = product.product_id,
										Time = order.Time,
										Quantity = orderInfo.Quantity,
										TotalPrice = order.total_price,
										OrderStatus = order.order_status,

									}).ToList();

			//// Lấy danh sách đơn hàng của người dùng đã thanh toán thất bại
			//var failedOrders = _context.orders
			//	.Where(o => o.user_id == Functions._UserID && o.order_status == "failed")
			//	.ToList();

			//// Hiển thị thông tin đơn hàng thành công và thất bại
			return View(successfulOrders);
		}

		public IActionResult Details(int id)
		{
			// Lấy thông tin chi tiết của đơn hàng
			var orderDetails = _context.orderinfos
				.Where(oi => oi.OrderID == id)
				.ToList();

			// Hiển thị chi tiết đơn hàng
			return View(orderDetails);
		}

		public IActionResult PaymentSuccess(int orderId)
		{
			// Cập nhật trạng thái đơn hàng thành công
			var order = _context.orders.FirstOrDefault(o => o.order_id == orderId);
			if (order != null)
			{
				order.order_status = "paid";
				_context.SaveChanges();
				return View(order);
			}
			return NotFound();
		}

		public IActionResult PaymentFail(int orderId)
		{
			// Cập nhật trạng thái đơn hàng thất bại
			var order = _context.orders.FirstOrDefault(o => o.order_id == orderId);
			if (order != null)
			{
				order.order_status = "failed";
				_context.SaveChanges();
				return View(order);
			}
			return NotFound();
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
//public IActionResult Index(Dictionary<string, string> request)
//{
//	if (!Functions.IsLogin())
//		return RedirectToAction("Index", "login");
//	var order = _context.orders.Where(o => o.user_id == Functions._UserID).Select(o => new
//	{
//		order = o,
//		product = _context.products.Where(p => p.product_id == o.product_id).FirstOrDefault(),
//	}).ToList();

//	//return Ok(order);
//	return View(order);
//}
//[HttpPost]
//public ActionResult AddToCart(int product_id, int quantity, int userid)
//{
//	if (!Functions.IsLogin())
//		return RedirectToAction("Index", "login");
//	var infor = "";
//	var listorder = _context.orders.Where(m => m.user_id == userid).ToList();
//	var product = _context.products.FirstOrDefault(p => p.product_id == product_id);
//	if (product == null)
//	{
//		return NotFound();
//	}
//	decimal total_price = (decimal)(product.price * quantity);
//	var order = new Models.Order
//	{
//		user_id = Functions._UserID,
//		seller_id = product.seller_id,
//		product_id = product_id,
//		quantity = quantity,
//		total_price = total_price,
//		order_status = "Pending",
//	};
//	_context.orders.Add(order);
//	_context.SaveChanges();
//	return RedirectToAction("Index");

//}
//public ActionResult Pay()
//{
//	int userId = Functions._UserID;
//	var listCart = _context.orders.Where(m => m.user_id == userId).ToList();
//	decimal totalPay = 0;
//	var productIds = new StringBuilder();

//	foreach (var item in listCart)
//	{
//		var product = _context.products.FirstOrDefault(m => m.product_id == item.product_id);
//		if (product != null)
//		{
//			totalPay += product.price;
//			productIds.Append(item.).Append(",");
//		}
//	}

//	var now = DateTime.Now;
//	Order newOrder = new Order()
//	{
//		user_id = userId,
//		total_price = totalPay,
//		Time = now,
//		order_status = "oe"
//	};

//	_context.Add(newOrder);
//	_context.SaveChanges();

//	var orderId = 1;

//	//foreach (var item in listCart)
//	//{
//	//	_context.Add(new OrderInfo()
//	//	{
//	//		OrderID = orderId,
//	//		ProductID = item.product_id,
//	//		OrderStatus = "oe",
//	//	});
//	//}

//	_context.SaveChanges();
//	return Redirect($"/vnpayapi/{totalPay}00&{productIds.ToString().TrimEnd(',')}&{orderId}");
//}

//public ActionResult Pay(int userid)
//{
//		var listcart = _context.orders.Where(m => m.user_id == userid).ToList();
//		decimal totalpay = 0;
//		var infor = "";
//		foreach (var item in listcart)
//		{
//			totalpay += _context.products.Where(m => m.product_id == item.product_id).FirstOrDefault().product_id;
//			infor += item.product_id;
//		}
//		var now = DateTime.Now;
//		Order neworder = new Order()
//		{
//			user_id = userid,
//			total_price = totalpay,
//			Time = now
//		};
//		_context.Add(neworder);
//		_context.SaveChanges();
//		var orderid = _context.orders.Where(m => m.user_id == userid && m.total_price == totalpay).FirstOrDefault().order_id;
//		var n = from m in listcart
//				select _context.Add(new OrderInfo()
//				{
//					OrderID = orderid,
//					ProductID = m.product_id,
//					OrderStatus = "oe",
//				});
//		_context.SaveChanges();
//		return Ok(new
//		{
//			code = 1,
//			messenger = $"/vnpayapi/{totalpay}00&{infor}&{orderid}2"
//		});
//}	