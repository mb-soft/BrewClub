using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Website.Settings
{
	public interface ILanguageSettings
	{

		string BaseCultureCode { get; }
		string LanguageFilesDirectory { get; }

	}
}
