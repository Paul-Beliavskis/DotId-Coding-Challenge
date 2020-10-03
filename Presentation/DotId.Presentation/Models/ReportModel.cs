using System.ComponentModel;

namespace DotId.Presentation.Models
{
    public class ReportModel
    {
        public int? Disadvantage2011 { get; set; }
        public int? Disadvantage2016 { get; set; }
        [DisplayName("Compare 2016 to 2011")]
        public int Comparison
        {
            get
            {
                return Disadvantage2016.GetValueOrDefault() - Disadvantage2011.GetValueOrDefault();
            }
        }
        public string PlaceName { get; set; }
        public string StateName { get; set; }
    }
}
