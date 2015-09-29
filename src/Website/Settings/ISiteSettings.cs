using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace mbsoft.BrewClub.Website.Settings
{
	public interface ISiteSettings
	{
		ILanguageSettings Language { get; }

		string AuthenticationType { get; }
	}
}