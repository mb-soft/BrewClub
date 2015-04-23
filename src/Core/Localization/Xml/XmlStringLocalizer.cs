using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace mbsoft.BrewClub.Localization.Xml
{
	public class XmlStringLocalizer : IStringLocalizer
	{
		// load the base language and the current one
		// base language gets loaded up once and can be re-used for all requests

		public const string PathFormat = @"{0}\{1}.xml";

		private string languageFilesDirectory;
		private string baseCultureCode;

		private Dictionary<string, Language> languages;

		private XmlStringLocalizer()
		{

		}

		public static XmlStringLocalizer Create(string baseCultureCode, string languageFilesDirectory)
		{
			var baseLanguage = LoadLanguage(baseCultureCode, languageFilesDirectory);
			var languages = new Dictionary<string, Language>();
			languages.Add(baseCultureCode, baseLanguage);

			var instance = new XmlStringLocalizer()
			{
				baseCultureCode = baseCultureCode,
				languageFilesDirectory = languageFilesDirectory,
				languages = languages,
			};

			return instance;
		}

		public string Localize(string s, string[] cultureCodes)
		{
			return GetLocalizedValue(s, cultureCodes);
		}

		public string LocalizeFormat(string formatKey, string[] cultureCodes, params object[] args)
		{
			var format = GetLocalizedValue(formatKey, cultureCodes);

			return string.Format(format, args);
		}

		private string GetLocalizedValue(string key, string[] cultureCodes)
		{
			foreach (var code in cultureCodes)
			{
				var language = GetLanguage(code);

				if (language.ContainsKey(key))
				{
					return language.GetWord(key);
				}
			}

			// fall back to base language and throw an exception if key is missing
			var baseLanguage = GetLanguage(baseCultureCode);

			if (!baseLanguage.ContainsKey(key))
			{
				throw new MissingLocalizedStringException(key, baseCultureCode);
			}
			else
			{
				return baseLanguage.GetWord(key);
			}			
		}

		private Language GetLanguage(string cultureCode)
		{
			if (!languages.ContainsKey(cultureCode))
			{
				languages.Add(cultureCode, LoadLanguage(cultureCode, languageFilesDirectory));
			}

			return languages[cultureCode];
		}


		public static Language LoadLanguage(string cultureCode, string languageFilesDirectory)
		{
			var serializer = new XmlSerializer(typeof(Language));

			var path = string.Format(PathFormat, languageFilesDirectory, cultureCode);

			try
			{
				using (var input = new StreamReader(path))
				{
					return (Language)serializer.Deserialize(input);
				}
			}
			catch
			{
				// file doesn't exist, load it up as an empty language
				return new Language();
			}

		}
	}
}
