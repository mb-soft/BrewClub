using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Models
{
	[Serializable]
	public class UserState
	{

		public string Username { get; private set; }

		public int UserId { get; private set; }

		public bool IsAdmin { get; private set; }

		public bool IsAnonymous { get; private set; }

		public string DisplayName { get; private set; }


		private UserState()
		{
		}

		public static UserState Create(string username, bool isAdmin, string displayName, int userId)
		{
			return new UserState
			{
				DisplayName = displayName,
				IsAdmin = isAdmin,
				Username = username,
				IsAnonymous = false,
				UserId = userId,
			};
		}

		public static UserState Anonymous
		{
			get { return new UserState { DisplayName = "", IsAdmin = false, Username = "", IsAnonymous = true }; }
		}

	}
}
