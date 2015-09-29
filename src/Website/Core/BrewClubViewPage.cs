using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mbsoft.BrewClub.Data;
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

		public string PageTitle
		{
			get { return (string)ViewBag.PageTitle; }
			set { ViewBag.PageTitle = value; }
		}

		public ISiteSettings Settings
		{
			get { return (ISiteSettings)ViewBag.SiteSettings; }
		}

		public User CurrentUser
		{
			get { return (User)ViewBag.CurrentUser; }
		}

		public bool IsAnonymous
		{
			get { return CurrentUser == null; }
		}
	}

	public abstract class BrewClubViewBase : BrewClubViewPage<object>
	{
	}
}