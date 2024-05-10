using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Components
{
    [ViewComponent(Name = "ServicesView")]
    public class ServicesViewComponent : ViewComponent
    {
        private DataContext _context;
        public ServicesViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listcc = (from m in _context.services
                          select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listcc));
        }
    }
}
