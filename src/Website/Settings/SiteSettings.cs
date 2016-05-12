using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using mdryden.Core.Settings;
using Microsoft.AspNet.Identity;

namespace mbsoft.BrewClub.Website.Settings
{
	public class SiteSettings : ISiteSettings
	{
		public ILanguageSettings Language { get; private set; }

		public string AuthenticationType { get; private set; }


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
			newInstance.AuthenticationType = LoadAndValidateAuthenticationType(); 

			return newInstance;
		}

		private static string LoadAndValidateAuthenticationType()
		{
			var type = SettingsHelper.LoadRequiredAppSetting("AuthenticationType");

			switch (type)
			{
				case DefaultAuthenticationTypes.ApplicationCookie:
				case DefaultAuthenticationTypes.ExternalBearer:
				case DefaultAuthenticationTypes.ExternalCookie:
				case DefaultAuthenticationTypes.TwoFactorCookie:
				case DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie:
					return type;

				default:
					throw new ConfigurationErrorsException("AuthenticationType is not valid, see DefaultAuthenticationTypes constants for list");
            }
        }
	}
}