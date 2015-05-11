using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class RecipeReview
	{

		public UserProfile Author { get; set; }

		public string Comments { get; set; }
		
		public bool AuthorBrewedIt { get; set; }
			

	}
}
