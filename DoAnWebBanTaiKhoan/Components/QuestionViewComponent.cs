using DoAnTapHoaCongNghe.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnTapHoaCongNghe.Components
{
    [ViewComponent(Name = "QuestionView")]
    public class QuestionViewComponent : ViewComponent
    {
        private DataContext _context;
        public QuestionViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listqt = (from m in _context.question
                               select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listqt));
        }
    }
}
