using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Utilities;
using DoAnTapHoaCongNghe.Areas.VNPayAPI.Util;
using NuGet.Packaging;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
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
		public ActionResult AddToCart(int product_id, int quantity)
		{
			if (!Functions.IsLogin())
				return RedirectToAction("Index", "login");

			var product = _context.products.FirstOrDefault(p => p.product_id == product_id);
			if (product == null)
			{
				return NotFound();
			}

			// Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
			var existingCartItem = _context.carts.FirstOrDefault(c => c.product_id == product_id && c.user_id == Functions._UserID);
			if (existingCartItem != null)
			{
				// Cập nhật số lượng
				existingCartItem.quantity += quantity;
				TempData["InfoMessage"] = "Đã cập nhật số lượng sản phẩm trong giỏ hàng.";
			}
			else
			{
				// Nếu sản phẩm chưa có trong giỏ hàng, thêm mới vào 
				var order = new Models.Cart 
				{
					user_id = Functions._UserID,
					product_id = product_id,
					quantity = quantity,
				};
				_context.Add(order);
				TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng.";
			}
			_context.SaveChanges();
			return RedirectToAction("Index", "Product");
		}
		public ActionResult Pay()
        {
            int userId = Functions._UserID;
            var carts = _context.carts.Where(m => m.user_id == userId).ToList();
            decimal totalPay = 0;
            string productstr = "";
            List<int> productIds = new List<int>();
            List<int> sellerIds = new List<int>();
			foreach (var item in carts)
            {
                var product = _context.products.FirstOrDefault(m => m.product_id == item.product_id);
                if (product != null)
                {
                    totalPay += product.price;
                    productstr += product.product_id + ",";
                    productIds.Add(product.product_id);
					sellerIds.Add(product.seller_id);
				}
            }
			string url = hanldeOrder(userId, totalPay, carts, productstr, sellerIds);
            return Redirect(url);
        }

        public string hanldeOrder(int userId, decimal totalPay, List<Cart> carts, string productstr, List<int> sellerIds)
        {
			var now = DateTime.Now;
            List<int> idOrders = new List<int>();
            foreach(int id in sellerIds)
            {
				Order newOrder = new Order()
				{
					user_id = userId,
					seller_id = id,
					total_price = totalPay,
					Time = now,
					order_status = "paid"
				};

				_context.Add(newOrder);
				_context.SaveChanges();
				idOrders.Add(newOrder.order_id);
			}

            int index = 0;
			foreach (var cart in carts)
			{
				_context.Add(new OrderInfo()
				{
					OrderID = idOrders[index],
					ProductID = cart.product_id,
					Quantity = cart.quantity,
				});
                ++index;
			}
			_context.SaveChanges();
			return $"/vnpayapi/{Convert.ToInt32(totalPay)}00&{productstr.TrimEnd(',')}&{idOrders[index - 1]}";
		}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
				return NotFound();
			}
            var cc = _context.carts.Find(id);
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
            var db = _context.carts.Find(id);
			if (db == null)
            {
                return NotFound();
            }
            _context.carts.Remove(db);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

	}
}
