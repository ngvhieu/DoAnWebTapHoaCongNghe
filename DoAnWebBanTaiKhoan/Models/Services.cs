using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnTapHoaCongNghe.Models
{
    [Table("Services")]
    public class Services
    {
        [Key]
        public int services_id { get; set; }
        public string? services_name { get; set; }
        public string? services_info { get; set; }
        public string? image { get; set; }
        public string? link { get; set; }
    }
}
