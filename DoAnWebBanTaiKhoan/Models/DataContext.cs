using DoAnTapHoaCongNghe.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using DoAnTapHoaCongNghe.Areas.Seller.Models;
namespace DoAnTapHoaCongNghe.Models
{
    public class DataContext : DbContext
    {
        internal object Category;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SellerMenu> sellerMenus { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }    
        public DbSet<admin> admins { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Seller> sellers { get; set; }
		public DbSet<Order> orders { get; set; }
		public DbSet<OrderInfo> orderinfos { get; set; }
		public DbSet<Cart> carts { get; set; }
        public DbSet<Footer> footer { get; set; }
        public DbSet<Question> question { get; set; }
        public DbSet<Services> services { get; set; }
        public DbSet<ProductDetail> productdetail { get; set; }
	}
}
