using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DoAnTapHoaCongNghe.Models
{
	[Table("HeroSection")]
	public class HeroSection
	{
		[Key]
		public int Id { get; set; }
		public string? Intro { get; set; }
		public string? Info { get; set; }
		public string? Image {  get; set; }
		public string? Topic { get; set; }
		public int Topicdata { get; set; }
	}
}
