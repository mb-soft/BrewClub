using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class ClassifiedQuestion
	{

		public UserProfile Author { get; set; }

		public ClassifiedAnswer Answer { get; set; }

	}
}
