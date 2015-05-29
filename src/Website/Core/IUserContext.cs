using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Models;

namespace mbsoft.BrewClub.Website
{
	public interface IUserContext
	{
		void SetUserState(UserState state);
		UserState GetUserState();
	}
}