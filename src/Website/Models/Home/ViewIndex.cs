using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Website.Models.Shared;

namespace mbsoft.BrewClub.Website.Models.Home
{
	public class ViewIndex
	{

		public ViewArticleSummary FeaturedArticle { get; set; }

		public IEnumerable<ViewArticleSummary> PopularArticles { get; set; }

	}
}