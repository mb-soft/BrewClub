﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Data;
using Microsoft.AspNet.Identity;

namespace mbsoft.BrewClub.Website
{
	public class UserRetriever
	{

		private BrewClubUserManager userManager;

		public UserRetriever(BrewClubUserManager userManager)
		{
			this.userManager = userManager;
		}

		public bool TryGetAuthenticatedUser(string usernameEmail, string password, out User user)
		{
			var isEmailAddress = usernameEmail.Contains("@");

			user = null;

			if (isEmailAddress)
			{
				var userByEmail = userManager.FindByEmail(usernameEmail);
				if (userByEmail != null)
				{
					user = userManager.Find(userByEmail.UserName, password);
				}
			}
			else
			{
				user = userManager.Find(usernameEmail, password);
			}

			return user != null;
		}

	}
}