using DoAnTapHoaCongNghe.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
namespace DoAnTapHoaCongNghe.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Menu> Menus { get; set; }
        
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }    
    }
}
