using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Models;
namespace DoAnTapHoaCongNghe.Areas.Admin.Components
{
    [ViewComponent(Name = "AdminMenu")]
    public class AdminMenuComponent : ViewComponent
    {
        private readonly DataContext _context;
        public AdminMenuComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from cc in _context.AdminMenus
                          where (cc.IsActive == true)
                          select cc).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
