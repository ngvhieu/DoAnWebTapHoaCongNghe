using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnTapHoaCongNghe.Models
{
	[Table("product_detail")]
	public class ProductDetail
	{
		//[Key]
		public int product_id { get; set; }
		[Key]
		public int product_code { get; set; }
		public string? value { get; set; }
		public string? detail { get; set; }
		public string? status { get; set; }
	}
}
