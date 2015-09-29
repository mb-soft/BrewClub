using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class ClassifiedQuestion
	{
		public int ClassifiedQuestionId { get; set; }

		public User Author { get; set; }

		public string Question { get; set; }

		public ClassifiedAnswer Answer { get; set; }

	}
}
