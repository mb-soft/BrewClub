using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mbsoft.BrewClub.Localization
{
	public interface IStringLocalizer
	{

		string Localize(string s, string[] cultureCodes);
		string LocalizeFormat(string formatKey, string[] cultureCodes, params object[] args);

	}
}
