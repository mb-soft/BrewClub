using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Website.Settings;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
				AuthenticationType = SiteSettings.GetInstance().AuthenticationType,
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