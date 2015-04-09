using System.Web;
using System.Web.Mvc;

namespace mbsoft.BrewClub.Website
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
