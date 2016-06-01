using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Models;
using mbsoft.BrewClub.Website.Models.Account;
using mbsoft.BrewClub.Website.Settings;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace mbsoft.BrewClub.Website.Controllers
{
	public class AccountController : ControllerBase
	{
		private BrewClubUserManager userManager;

		public AccountController(Data.BrewClubDbContext dataContext, ISiteSettings siteSettings, BrewClubUserManager userManager)
			: base(dataContext, siteSettings)
		{
			this.userManager = userManager;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && userManager != null)
			{
				userManager.Dispose();
			}

			base.Dispose(disposing);
		}

		private IAuthenticationManager GetAuthorizationManager()
		{
			var context = Request.GetOwinContext();
			var authManager = context.Authentication;

			return authManager;
		}

		private string GetRedirectUrl(string returnUrl)
		{
			return string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home") : returnUrl;
		}

		[GET("login")]
		public ActionResult LoginRegister(string returnUrl)
		{
			var login = new SubmitLogin { ReturnUrl = returnUrl };
			var registration = new SubmitRegistration();

			var model = new SubmitLoginRegistration { Login = login, Registration = registration };

			return View("Login", model);
		}

		[POST("login")]
		public ActionResult Login(SubmitLogin model)
		{

			if (!ModelState.IsValid)
			{
				return View("_Login", model);
			}
			
			var retriever = new UserRetriever(userManager);

			User authenticatedUser;
			
			if (!retriever.TryGetAuthenticatedUser(model.UsernameEmail, model.Password, out authenticatedUser))
			{
				ModelState.AddModelError("", GetLocalizer().Localize("Invalid email or password"));
				return View("_Login", model);
			}

			// found user
			var identity = userManager.CreateIdentity(authenticatedUser, SiteSettings.GetInstance().AuthenticationType);

			GetAuthorizationManager().SignIn(identity);

			return Redirect(GetRedirectUrl(model.ReturnUrl));
		}

		[GET("logout")]
		public ActionResult Logout()
		{
			GetAuthorizationManager().SignOut(SiteSettings.GetInstance().AuthenticationType);

			return RedirectToAction("LoginRegister");
		}

		[POST("register")]
		public ActionResult Register(SubmitRegistration model)
		{
			if (!ModelState.IsValid)
			{
				return View("_Register", model);
			}
			
			var foundByEmail = dataContext.Users.FirstOrDefault(u => u.Email.Equals(model.EmailAddress, StringComparison.InvariantCultureIgnoreCase));

			if (foundByEmail != null)
			{
				ModelState.AddModelError("", GetLocalizer().Localize("Email address is already in use"));
				return View("_Register", model);
			}

			var foundByUsername = dataContext.Users.FirstOrDefault(u => u.UserName.Equals(model.UserName, StringComparison.InvariantCultureIgnoreCase));

			if (foundByUsername != null)
			{
				ModelState.AddModelError("", GetLocalizer().Localize("User name is not available"));
				return View("_Register", model);
			}

			userManager.Create(model.UserName, model.EmailAddress, model.Password);

			var login = new SubmitLogin { UsernameEmail = model.EmailAddress, Password = model.Password };

			return Login(login);
		}

		[GET("u/{username}")]
		public ActionResult ViewProfile(string username)
		{
			throw new NotImplementedException();
		}

	}
}