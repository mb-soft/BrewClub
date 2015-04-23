using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Localization;

namespace mbsoft.BrewClub.Website
{
	public class SiteLocalizer
	{

		private IStringLocalizer underlyingLocalizer;
		private string baseCultureCode;

		public SiteLocalizer(string baseCultureCode, IStringLocalizer underlyingLocalizer)
		{
			this.baseCultureCode = baseCultureCode;
			this.underlyingLocalizer = underlyingLocalizer;
		}

		private string[] GetCultureCodes()
		{
			if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length > 0)
			{
				return HttpContext.Current.Request.UserLanguages;
			}
			else
			{
				return new string[] { baseCultureCode };
			}
		}


		public string Localize(string s)
		{
			return underlyingLocalizer.Localize(s, GetCultureCodes());
		}

		public string LocalizeFormat(string formatKey, params object[] args)
		{
			return underlyingLocalizer.LocalizeFormat(formatKey, GetCultureCodes(), args);
		}

	}
}