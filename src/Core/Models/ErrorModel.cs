using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Models
{
	public class ErrorModel
	{
		public string Url { get; set; }

		public string Message { get; set; }

		public Exception Exception { get; set; }

	}
}
