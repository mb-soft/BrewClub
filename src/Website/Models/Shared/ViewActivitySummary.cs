using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Shared
{
	public class ViewActivitySummary
	{
		public string Title { get; set; }

		public int AuthorId { get; set; }

		public string AuthorName { get; set; }

		public object ActivtyType { get; set; }

		public DateTime DateCreated { get; set; }
	}
}