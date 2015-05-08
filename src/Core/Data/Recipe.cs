using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class Recipe : PostedItem
	{

		public string Description { get; set; }

		// alot more needs to be added here.

		public virtual ICollection<RecipeReview> Reviews { get; set; }

	}
}
