using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Models
{
	[Table("Order")]
	public class Order
	{
		[Key]
		public int order_id { get; set; }
		public int user_id { get; set; }
		public int seller_id { get; set; }
		public int product_id { get; set; }
		public int quantity { get; set; }
		public decimal total_price { get; set; }
		public string? order_status { get; set; }
		//public DateTime? created_at { get; set; }
	}
}
