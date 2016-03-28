using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using mbsoft.BrewClub.Models;

namespace mbsoft.BrewClub.Website.Controllers
{
	public class AccountController : ControllerBase
	{
		public AccountController(Data.BrewClubContext dataContext, IUserContext context, Settings.ISiteSettings siteSettings)
			: base(dataContext, context, siteSettings)
		{

		}

		[GET("login")]
		public ActionResult Login()
		{
			throw new NotImplementedException();
		}

		[POST("login")]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			throw new NotImplementedException();
		}

		[GET("register")]
		public ActionResult Register()
		{
			throw new NotImplementedException();
		}

	}
}