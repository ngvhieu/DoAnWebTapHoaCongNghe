using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Models;
namespace DoAnTapHoaCongNghe.Areas.Seller.Components
{
    [ViewComponent(Name = "SellerMenu")]
    public class SellerMenuComponent : ViewComponent
    {
        private readonly DataContext _context;
        public SellerMenuComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menuu = (from cc in _context.sellerMenus
                          where (cc.IsActive == true)
                          select cc).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", menuu));
        }
    }
}
