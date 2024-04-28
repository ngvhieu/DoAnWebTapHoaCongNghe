using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace DoAnTapHoaCongNghe.Models
{
	[Table("OrderInfo")]
	public class OrderInfo
	{
		[Key]
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public int Quantity { get; set; }
	}
}
