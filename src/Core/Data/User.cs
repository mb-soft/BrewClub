using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mbsoft.BrewClub.Data
{
	public class User : IdentityUser
	{
		
		public string FullName { get; set; }

		public bool ShowFullName { get; set; }

		public bool IsInRole(string roleName)
		{
			var role = this.Roles.FirstOrDefault(r => r.RoleId == roleName);

			return role != null;
		}

		public bool IsAdmin
		{
			get { return IsInRole(BrewClubRoleManager.Admin); }
		}

		public bool IsModerator
		{
			// admins are always moderators
			get { return IsInRole(BrewClubRoleManager.Moderator) || IsAdmin; }
		}

		public bool IsMember
		{
			// admins and moderators are always mods
			get { return IsInRole(BrewClubRoleManager.Member) || IsAdmin || IsModerator; }
		}

		public bool IsUser
		{
			// admins, mods and members are always users
			get { return IsInRole(BrewClubRoleManager.User) || IsAdmin || IsModerator || IsMember; }
		}
	}
}
