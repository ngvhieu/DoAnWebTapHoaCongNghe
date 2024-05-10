using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Models
{
	[Table("Cart")]
	public class Cart
	{
		[Key]
		public int cart_id { get; set; }
		public int user_id { get; set; }
		public int product_id { get; set; }
		//public Product product { get; set; }
		public int quantity { get; set; }
		public DateTime Time { get; set; }
		
		public Cart() { Time = DateTime.Now; }
    }
}
