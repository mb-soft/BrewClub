using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public static class BrewClubRoleManager
	{

		public static string Admin = "Admin"; // has rights to all functions
		public static string Moderator = "Moderator"; // has rights to moderation functions
		public static string Member = "Member"; // allowed to post content
		public static string User = "User"; // allowed to comment on comment and maintain a profile

	}
}
