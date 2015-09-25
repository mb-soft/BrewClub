using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using mbsoft.BrewClub.Models;
using mbsoft.BrewClub.Website.Models.Account;

namespace mbsoft.BrewClub.Website.Controllers
{
	public class AccountController : ControllerBase
	{
		public AccountController()
			: base(GetDefaultUserContext(), GetDefaultSiteSettings())
		{

		}

		[GET("login")]
		public ActionResult Login(string returnUrl)
		{
			var model = new EditLogin { ReturnUrl = returnUrl };

			return View("Login", model);
		}

		[POST("login")]
		public ActionResult PostLogin(EditLogin model)
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