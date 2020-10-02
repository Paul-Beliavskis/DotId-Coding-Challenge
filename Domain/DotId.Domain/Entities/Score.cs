using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotId.Domain.Entities
{
	public class Score
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ScoreId { get; set; }
		public int? DisadvantageScore { get; set; }
		public int? AdvantageDisadvantageScore { get; set; }
		public int Year { get; set; }
		public Location Location { get; set; }
	}
}
