using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public static class BrewClubRoleManager
	{

		/// <summary>
		/// Has rights to all functions
		/// </summary>
		public static string Admin = "Admin"; // 

		/// <summary>
		/// Has rights to moderation functions
		/// </summary>
		public static string Moderator = "Moderator";

		/// <summary>
		/// Allowed to post content
		/// </summary>
		public static string Member = "Member";

		/// <summary>
		/// Allowed to comment on articles and maintain a profile
		/// </summary>
		public static string User = "User"; 

	}
}
