﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{
	public class HomeController : Controller
	{
		[GET("")]
		public ActionResult Index()
		{
			return View();
		}

		[GET("about")]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

	}
}