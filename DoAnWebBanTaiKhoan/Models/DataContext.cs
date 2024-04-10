using DoAnTapHoaCongNghe.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
namespace DoAnTapHoaCongNghe.Models
{
    public class DataContext : DbContext
    {
        internal object Category;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Menu> Menus { get; set; }
        
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }    
        public DbSet<admin> admins { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Seller> sellers { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
