using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DoAnTapHoaCongNghe.Utilities;
namespace DoAnTapHoaCongNghe.Controllers
{
    public class ProductController : Controller
    {
		private readonly DataContext _dataContext;
		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
		{
			var listofProduct = (from m in _dataContext.products
								 select m).ToList();
			return View(listofProduct);
		}
		public IActionResult Details(int id)
		{
			var product = _dataContext.products.FirstOrDefault(p => p.product_id == id);
			if (product == null)
			{
				return NotFound();
			}
			var relatedProducts = _dataContext.products.Where(p => p.category == product.category && p.product_id != id).Take(5).ToList();
			//return Ok(new List<Product>());
			if (relatedProducts != null)
			{
				ViewBag.RelatedProducts = relatedProducts;
			}
			else
			{
				ViewBag.RelatedProducts = new List<Product>();
			}
			var seller = _dataContext.sellers.FirstOrDefault(s => s.seller_id == product.seller_id);

			ViewBag.SellerName = seller.shop_name;
			ViewBag.SellerDescription = seller.shop_description;

			return View(product);
		}
	}
}
