using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mbsoft.BrewClub.Localization;
using mbsoft.BrewClub.Models;
using mbsoft.BrewClub.Website;
using mbsoft.BrewClub.Website.Settings;

namespace mbsoft.BrewClub.Website
{
	public abstract class BrewClubViewPage<T> : WebViewPage<T>
	{
		public SiteLocalizer Localizer
		{
			get { return (SiteLocalizer)ViewBag.Localizer; }
		}

		public string PageTitle { get; set; }

		public ISiteSettings Settings
		{
			get { return (ISiteSettings)ViewBag.SiteSettings; }
		}

		public UserState UserState
		{
			get { return (UserState)ViewBag.UserState; }
		}
	}

	public abstract class BrewClubViewBase : BrewClubViewPage<object>
	{
	}
}