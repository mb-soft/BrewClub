using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{
    [RoutePrefix("contact")]
    public class ContactController : ControllerBase
    {
        public ContactController(Data.BrewClubDbContext dataContext, Settings.ISiteSettings siteSettings)
			: base(dataContext, siteSettings)
		{

		}

        public ActionResult Contact()
        {
            return View();
        }
    }
}