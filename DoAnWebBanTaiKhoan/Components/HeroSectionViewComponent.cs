using Microsoft.AspNetCore.Mvc;
using DoAnTapHoaCongNghe.Models;
namespace DoAnTapHoaCongNghe.Components
{
	[ViewComponent(Name = "HeroSectionView")]
	public class HeroSectionViewComponent : ViewComponent	
	{
		private DataContext _context;
		public HeroSectionViewComponent(DataContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var listofHero = (from m in _context.HeroSections
							  select m).ToList();
			return await Task.FromResult((IViewComponentResult)View("Default", listofHero));
		}
	}
}
