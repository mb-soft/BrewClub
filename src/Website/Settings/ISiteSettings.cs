using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Settings
{
	public interface ISiteSettings
	{
		ILanguageSettings Language { get; }
	}
}