using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Areas.Admin.Models
{
    [Table("Admin")]
    public class admin
    {
        [Key]
        public int admin_id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public int? role { get; set; }
    }
}
