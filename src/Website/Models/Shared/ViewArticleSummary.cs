using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Shared
{
	public class ViewArticleSummary
	{
		public int NewsId { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public int AuthorId { get; set; }

		public string AuthorName { get; set; }

		public DateTime DateCreated { get; set; }

		public int CommentCount { get; set; }
	}
}