using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotId.Domain.Entities
{
	public class Disadge
	{
		[Key]
		public Guid ScoreId { get; set; }
		public int? Disadvantage2011 { get; set; }
		public int? Disadvantage2016 { get; set; }
		public string PlaceName { get; set; }
		public string StateName { get; set; }
	}
}
