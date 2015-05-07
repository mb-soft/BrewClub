using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class PostedItem
	{

		public Author Author { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime LastEdit { get; set; }

	}
}
