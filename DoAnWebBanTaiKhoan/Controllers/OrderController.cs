using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Utilities;
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
			var successfulOrder = _context.orders.Where(m => Functions._UserID == m.user_id && m.order_status == "paid").ToList();


            successfulOrder = successfulOrder.OrderByDescending(o => o.Time).ToList();
            return View(successfulOrder);
		}
		public IActionResult Details(int OrderID)
		{
            var successfulOrders = (from order in _context.orders
                                    join orderInfo in _context.orderinfos on order.order_id equals orderInfo.OrderID
                                    join product in _context.products on orderInfo.ProductID equals product.product_id
                                    join seller in _context.sellers on product.seller_id equals seller.seller_id
                                    join productdetail in _context.productdetail on orderInfo.Product_Code equals productdetail.product_code
                                    where order.user_id == Functions._UserID && order.order_status == "paid" && productdetail.status =="sold"
                                    select new OrderViewModel
                                    {
                                        ShopName = seller.shop_name,
                                        OrderID = order.order_id,
                                        ProductName = product.product_name,
                                        ProductID = product.product_id,
                                        Quantity = orderInfo.Quantity,
                                        OrderStatus = order.order_status,
                                        Value = productdetail.value,
                                        Detail = productdetail.detail,
                                        ProductCode = productdetail.product_code,
                                    }).Where(m => m.OrderID == OrderID).ToList();
            return View(successfulOrders);
        }
	}
}
