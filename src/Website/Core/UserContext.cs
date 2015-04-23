using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Models;

namespace mbsoft.BrewClub.Website
{
	public class UserContext : IUserContext
	{

		const string UserStateSessionKey = "UserState";

		public void SetUserState(UserState state)
		{
			HttpContext.Current.Session[UserStateSessionKey] = state;
		}

		public UserState GetUserState()
		{
			return (UserState)HttpContext.Current.Session[UserStateSessionKey];
		}
	}
}