using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Components
{
    [ViewComponent(Name ="FooterView")]
    public class FooterViewComponent : ViewComponent
    {
        private DataContext _context;
        public FooterViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listoffuter = (from m in _context.footer
                              select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listoffuter));
        }
    }
}
