using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class ClassifiedAnswer
	{
		public int ClassifiedAnswerID { get; set; }

		public UserProfile Author { get; set; }

		public string Answer { get; set; }
	}
}
