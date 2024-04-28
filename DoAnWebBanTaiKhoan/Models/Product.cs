using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int product_id { get; set; }
        public int seller_id { get; set; }
        public string? product_name { get; set;}
        public string? product_description { get;
        set;}
        public decimal price { get; set; }
        public int quantity { get; set; }
        public string? category { get; set; }
        public string? image { get;set;}
        //public DateTime? created_at { get; set; }
    }
}
