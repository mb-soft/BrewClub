using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Localization
{
	public class MissingLocalizedStringException : Exception
	{

		public MissingLocalizedStringException(string key, string cultureCode)
			: base(message: string.Format("Missing localized string '{0}' for culture '{1}'", key, cultureCode))
		{

		}

	}
}
