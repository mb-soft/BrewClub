using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Models;

namespace mbsoft.BrewClub.Website
{
	public interface IUserContext
	{
		void SetUserState(User state);
		User GetUserState();
	}
}