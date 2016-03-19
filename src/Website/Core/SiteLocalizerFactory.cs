using mbsoft.BrewClub.Localization.Xml;
using mbsoft.BrewClub.Website.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website
{
    public class SiteLocalizerFactory
    {
        public ISiteLocalizer GetXmlStringSiteLocalizer(string baseCultureCode, string languageFilesDirectory)
        {
            // todo: tie this to a cache dependency to avoid unnecessary reloads, but still get updated when the file changes.
            var underlyingLocalizer = XmlStringLocalizer.Create(baseCultureCode, languageFilesDirectory);
            var localizer = new SiteLocalizer(baseCultureCode, underlyingLocalizer);
            return localizer;
        }

        public ISiteLocalizer GetXmlStringSiteLocalizer()
        {
            var settings = SiteSettings.GetInstance();
            return GetXmlStringSiteLocalizer(settings.Language.BaseCultureCode, settings.Language.LanguageFilesDirectory);
        }
    }
}