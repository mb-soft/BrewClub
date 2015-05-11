using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{
    public class ContactUsController : Controller
    {
        [GET("ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }
    }
}