using Microsoft.AspNetCore.Mvc;
using DoAnWebBanTaiKhoan.Models;
namespace DoAnWebBanTaiKhoan.Components
{
    [ViewComponent(Name = "MenuView")]
    public class MenuViewComponents : ViewComponent
    {
        private readonly DataContext _context;
        public MenuViewComponents(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofMenu = (from m in _context.Menus
                              where m.IsActive == true && m.Position == 1
                              select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));
        }
    }
}
