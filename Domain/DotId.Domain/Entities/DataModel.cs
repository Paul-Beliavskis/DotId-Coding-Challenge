using System.ComponentModel;

namespace DotId.Domain.Entities
{
    public class DataModel
    {
        public string PlaceName { get; set; }

        public string StateName { get; set; }

        public int Year { get; set; }

        [DisplayName("Disadvantage")]
        public int? DisadvantageScore { get; set; }

        [DisplayName("Advantage")]
        public int? AdvantageDisadvantageScore { get; set; }
    }
}
