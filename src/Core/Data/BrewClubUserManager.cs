using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mbsoft.BrewClub.Data
{
	public class BrewClubUserManager : UserManager<User>
	{
		private BrewClubUserManager(IUserStore<User> store) 
			: base(store)
		{
		}

		public static BrewClubUserManager Create(BrewClubDbContext context)
		{
			var userManager = new BrewClubUserManager(new UserStore<User>(context));

			userManager.UserValidator = new UserValidator<User>(userManager)
			{
				
			};

			return userManager;
		}
	}
}
