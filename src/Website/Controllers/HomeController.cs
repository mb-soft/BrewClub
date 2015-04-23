using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{
	public class HomeController : ControllerBase
	{

		public HomeController()
			: base(GetDefaultUserContext(), GetDefaultSiteSettings())
		{

		}

		[GET("")]
		public ActionResult Index()
		{
			return View();
		}

		[GET("contact")]
		public ActionResult Contact()
		{
			return View();
		}

	}
}