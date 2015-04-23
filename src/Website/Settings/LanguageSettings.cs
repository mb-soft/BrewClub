using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using mdryden.Core.Settings;

namespace mbsoft.BrewClub.Website.Settings
{
	public class LanguageSettings : ILanguageSettings
	{
		public string BaseCultureCode { get; private set; }

		public string LanguageFilesDirectory { get; private set; }



		private static LanguageSettings instance;
		private static object _lock = new object();

		public static LanguageSettings GetInstance()
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

		private static LanguageSettings CreateInstance()
		{
			var newInstance = new LanguageSettings();

			newInstance.BaseCultureCode = SettingsHelper.LoadRequiredAppSetting("Language_BaseCultureCode");
			newInstance.LanguageFilesDirectory = GetLanguageFilesDirectory();

			return newInstance;
		}

		private static string GetLanguageFilesDirectory()
		{
			var filesDir = SettingsHelper.LoadRequiredAppSetting("Language_FilesDirectory");

			if (filesDir.StartsWith(".") || filesDir.StartsWith("/") || filesDir.StartsWith("\\"))
			{
				// relative path, combine with executing directory
				filesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filesDir);
			}

			return filesDir;
		}
	}
}