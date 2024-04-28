using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? full_name { get; set; }
        public string? address { get; set; }
        public string? phone_number { get; set; }
        //public DateTime? create_at { get; set; }
        public string? avatar { get; set; }
    }
}
