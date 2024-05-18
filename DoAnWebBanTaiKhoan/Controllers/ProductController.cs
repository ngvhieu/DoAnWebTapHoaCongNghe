using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DoAnTapHoaCongNghe.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTapHoaCongNghe.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var listOfProducts = await _dataContext.products.ToListAsync();
            foreach (var product in listOfProducts)
            {
                await UpdateProductQuantity(product.product_id);
            }
            return View(listOfProducts); 
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProductQuantity(int productId)
        {
            var product = await _dataContext.products
                .FirstOrDefaultAsync(p => p.product_id == productId);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            product.quantity = await _dataContext.products
                .CountAsync(p => p.product_id == productId);

            var productDetails = await _dataContext.productdetail
                .Where(pd => pd.product_id == productId)
                .ToListAsync();

            int availableCount = productDetails.Count(pd => pd.status == "available");
            product.quantity = availableCount;
            _dataContext.products.Update(product);
            await _dataContext.SaveChangesAsync();

            return Ok("Quantity updated successfully.");
        }


        public IActionResult Details(int id)
        {
            var product = _dataContext.products.FirstOrDefault(p => p.product_id == id);
            if (product == null)
            {
                return NotFound();
            }

            var relatedProducts = _dataContext.products
                .Where(p => p.category == product.category && p.product_id != id)
                .Take(5)
                .ToList();

            if (relatedProducts != null)
            {
                ViewBag.RelatedProducts = relatedProducts;
            }
            else
            {
                ViewBag.RelatedProducts = new List<Product>();
            }

            var seller = _dataContext.sellers.FirstOrDefault(s => s.seller_id == product.seller_id);
            if (seller != null)
            {
                ViewBag.SellerName = seller.shop_name;
                ViewBag.SellerDescription = seller.shop_description;
            }

            return View(product);
        }
    }
}
