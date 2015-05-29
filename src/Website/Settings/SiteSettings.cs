using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Settings
{
	public class SiteSettings : ISiteSettings
	{
		public ILanguageSettings Language { get; private set; }



		private static SiteSettings instance;
		private static object _lock = new object();

		public static SiteSettings GetInstance()
		{
			if (instance == null)
			{
				lock (_lock)
				{
					instance = CreateInstance();
				}
			}

			return instance;
		}

		private static SiteSettings CreateInstance()
		{
			var newInstance = new SiteSettings();

			newInstance.Language = LanguageSettings.GetInstance();

			return newInstance;
		}
	}
}