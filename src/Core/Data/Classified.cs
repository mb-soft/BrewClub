using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class Classified : PostedItem
	{

		public string Body { get; set; }

		public int Price { get; set; }
		
		// phone number?

		public virtual ICollection<ClassifiedQuestion> Questions { get; set; }

	}
}
