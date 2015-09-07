using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mbsoft.BrewClub.Data;
using AttributeRouting.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{
    public class ArticlesController : ControllerBase
    {

        public ArticlesController()
			: base(GetDefaultUserContext(), GetDefaultSiteSettings())
		{

        }

        public const string ArticlesRouteUrl = "Articles";
        [GET(ArticlesRouteUrl)]
        public ActionResult Articles()
        {
            return View();
        }

    }
}
