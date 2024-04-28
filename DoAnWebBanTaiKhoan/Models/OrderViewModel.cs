namespace DoAnTapHoaCongNghe.ViewModels
{
	public class OrderViewModel
	{
		public int OrderID { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public string ShopName { get; set; }
		public int ProductID { get; set; }
		public DateTime? Time {  get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice {  get; set; }
		public string OrderStatus { get; set; }
		
	}
}
