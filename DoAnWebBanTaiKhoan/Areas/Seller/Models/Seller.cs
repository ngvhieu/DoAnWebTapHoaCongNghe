using DoAnTapHoaCongNghe.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnTapHoaCongNghe.Areas.Seller.Models
{
    [Table("Seller")]
    public class Seller
    {
        [Key]
        public int seller_id { get; set; }
        public string? shop_name { get; set; }
        public string? shop_description { get; set; }
        public int user_id { get; set; }
    }
}
