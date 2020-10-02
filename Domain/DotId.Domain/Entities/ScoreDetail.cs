using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotId.Domain.Entities
{
	public class ScoreDetail 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ScoreDetailsId { get; set; }
		public Score Score { get; set; }
		public int? AdvantageDisadvantageDecile { get; set; }
		public int? DisadvantageDecile { get; set; }
		public int? IndexOfEconomicResourcesScore { get; set; }
		public int? IndexOfEconomicResourcesDecile { get; set; }
		public int? IndexOfEducationAndOccupationScore { get; set; }
		public int? IndexOfEducationAndOccupationDecile { get; set; }
		public decimal UsualResedantPopulation { get; set; }
		
	}
}
