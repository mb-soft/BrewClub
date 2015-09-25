using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace mbsoft.BrewClub.Website
{
	public class OwinStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				Provider = new CookieAuthenticationProvider { OnApplyRedirect = ApplyLoginRedirect }
			});
		}

		private static void ApplyLoginRedirect(CookieApplyRedirectContext context)
		{
			var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
			var loginUrl = helper.Action("Login", "Account");

			context.Response.Redirect(loginUrl);
		}
	}
}