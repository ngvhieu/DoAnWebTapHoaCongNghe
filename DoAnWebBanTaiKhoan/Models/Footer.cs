using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Models
{
    [Table("Footer")]
    public class Footer
    {
        [Key]
		public int id { get; set; }
		public string? name { get; set; }
        public string? infor { get; set; }
        public string? phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }

    }
}
