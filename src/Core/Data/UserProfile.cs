using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class UserProfile
	{
		// I don't yet understand how membership and entity framework are supposed to integrate.
		public int UserProfileID { get; set; }

		public string DisplayName { get; set; }

		public string Email { get; set; }

	}
}
