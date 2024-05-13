using DoAnTapHoaCongNghe.Models;
using DoAnTapHoaCongNghe.Utilities;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAnTapHoaCongNghe.Controllers
{
    public class UserController : Controller
    {
		private readonly DataContext _dataContext;
		public UserController (DataContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index() 
		{
			var infoUser = (from m in _dataContext.users.Where(m=> m.user_id == Functions._UserID)
							select m).FirstOrDefault();
			if (!Functions.IsLogin())
				return RedirectToAction("Index", "login");
	     		var paidOrders = (from order in _dataContext.orders
							  join orderInfo in _dataContext.orderinfos on order.order_id equals orderInfo.OrderID
							  where order.user_id == Functions._UserID && order.order_status == "paid"
							  select new
							  {
								  TotalPrice = order.total_price,
								  Quantity = orderInfo.Quantity
							  }).ToList();
			decimal totalSpent = paidOrders.Sum(o => o.TotalPrice);
			int totalProductsBought = paidOrders.Sum(o => o.Quantity);
			ViewBag.TotalSpent = totalSpent;
			ViewBag.TotalProductsBought = totalProductsBought;
			return View(infoUser);
		}
    }
}
