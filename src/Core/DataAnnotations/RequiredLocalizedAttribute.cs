using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.DataAnnotations
{
	public class RequiredLocalizedAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
	{

		private string errorMessageString;

		public RequiredLocalizedAttribute(string errorMessageString)
		{
			this.errorMessageString = errorMessageString;
		}

		public override string FormatErrorMessage(string name)
		{
			//return Localizer
			throw new NotImplementedException();
		}

	}
}
