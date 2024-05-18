using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Utilities;
using DoAnTapHoaCongNghe.Areas.VNPayAPI.Util;
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

            // Ensure the requested quantity doesn't exceed the available amount
            if (quantity > product.quantity)
            {
                TempData["ErrorMessage"] = "Số lượng yêu cầu vượt quá số lượng sản phẩm có sẵn.";
                return RedirectToAction("Index", "Product"); // Or redirect to product page
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
                    totalPay += product.price * item.quantity;
                    productstr += product.product_id + ",";
                    productIds.Add(product.product_id);
                    sellerIds.Add(product.seller_id);
                }
            }
            //return hanldeOrder(userId, totalPay, carts, productstr, sellerIds);
            string url = hanldeOrder(userId, totalPay, carts, productstr, sellerIds);
            return Redirect(url);
        }
        public string hanldeOrder(int userId, decimal totalPay, List<Cart> carts, string productstr, List<int> sellerIds)
        {
            var now = DateTime.Now;
         
            
                Order newOrder = new Order
                {
                    user_id = userId,
                    seller_id = 0,
                    total_price = totalPay,
                    Time = now,
                    order_status = "paid"
                };
                _context.orders.Add(newOrder);
                _context.SaveChanges();
               
            
            // Xóa các OrderInfo hiện có của đơn hàng hiện tại
       var orderid = newOrder.order_id;
            // Tạo danh sách OrderInfo mới
            var newOrderInfos = new List<OrderInfo>();
           
                foreach (var cart in carts)
                {
                    var productDetails = _context.productdetail.Where(pd => pd.product_id == cart.product_id && pd.status == "available").Take(cart.quantity).ToList();
                    foreach (var productDetail in productDetails)
                    {
                        var orderInfo = new OrderInfo
                        {
                            OrderID = orderid,
                            Quantity = cart.quantity,
                            Product_Code = productDetail.product_code,
                            ProductID = cart.product_id,
                        };
                        // Kiểm tra xem OrderInfo đã tồn tại trong danh sách chưa
                        //if (!newOrderInfos.Any(oi => oi.OrderID == orderInfo.OrderID && oi.ProductID == orderInfo.ProductID))
                        //{
                        newOrderInfos.Add(orderInfo);
                        //}
                        //
                    }
                }
            
            _context.AddRange(newOrderInfos);
            _context.SaveChanges();
            var productIdsToUpdate = _context.productdetail.Where(oi => newOrderInfos.Select(x => x.Product_Code).Contains(oi.product_code)).ToList();
            foreach (var product in productIdsToUpdate)
            {
                product.status = "sold";
            }
            _context.productdetail.UpdateRange(productIdsToUpdate);
            _context.SaveChanges();
            return $"/vnpayapi/{Convert.ToInt32(totalPay)}00&{productstr.TrimEnd(',')}&{orderid}";
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = _context.carts.Find(cartId);
            if (cartItem != null)
            {
                cartItem.quantity = quantity;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
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
                TempData["ErrorMessage"] = "Không tìm thấy sản phẩm để xóa";
                return RedirectToAction("Index");
            }
            try
            {
                _context.carts.Remove(db);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi xóa sản phẩm: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}