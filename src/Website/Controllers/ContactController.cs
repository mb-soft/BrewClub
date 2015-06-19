using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{
    public class ContactController : ControllerBase
    {
        public ContactController()
			: base(GetDefaultUserContext(), GetDefaultSiteSettings())
		{

		}

        public const string ContactRouteUrl = "Contact";
        [GET(ContactRouteUrl)]
        public ActionResult Contact()
        {
            return View();
        }
    }
}