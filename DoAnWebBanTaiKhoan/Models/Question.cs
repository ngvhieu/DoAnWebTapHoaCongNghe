using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnTapHoaCongNghe.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int question_id { get; set; }
        public string? question {  get; set; }
        public string? answer { get; set; }
        public bool? is_active { get; set; }
    }
}
