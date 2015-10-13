using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Models;

namespace mbsoft.BrewClub.Website
{
	public class UserContext : IUserContext
	{

		const string UserStateSessionKey = "UserState";

		public void SetUserState(User state)
		{
			HttpContext.Current.Session[UserStateSessionKey] = state;
		}

		public User GetUserState()
		{
			return (User)HttpContext.Current.Session[UserStateSessionKey];
		}
	}
}