using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Exceptions;
using mbsoft.BrewClub.Localization;
using mbsoft.BrewClub.Localization.Xml;
using mbsoft.BrewClub.Models;
using mbsoft.BrewClub.Website;
using mbsoft.BrewClub.Website.Settings;

namespace mbsoft.BrewClub.Website.Controllers
{
	public abstract class ControllerBase : Controller
	{

		private DateTime requestStart;
        protected BrewClubDbContext dataContext { get; }
         protected ISiteSettings siteSettings { get; }


		public ControllerBase(BrewClubDbContext dataContext, ISiteSettings siteSettings)
		{
			requestStart = DateTime.Now;
            this.dataContext = dataContext;
			this.siteSettings = siteSettings;
		}


		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			// ** Supplemental data for views ** //
			ViewBag.CurrentUser = GetCurrentUser();
			ViewBag.Localizer = GetLocalizer();
			ViewBag.SiteSettings = siteSettings;


			// ** Exception Handling **//
			if (filterContext.Exception != null)
			{
				TryHandleException(filterContext);
			}

			ViewBag.RenderTime = DateTime.Now.Subtract(requestStart);

			base.OnActionExecuted(filterContext);
		}

		private void TryHandleException(ActionExecutedContext filterContext)
		{
			if (filterContext.Exception is LoginRequiredException)
			{
				var url = "/login";

				if (!string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Request.Url.PathAndQuery))
				{
					var path = filterContext.RequestContext.HttpContext.Request.Url.OriginalString;
					url += "?returnUrl=" + HttpContext.Server.UrlEncode(path);
				}

				filterContext.Result = Redirect(url);
				filterContext.ExceptionHandled = true;
				return;
			}


			if (filterContext.HttpContext.IsCustomErrorEnabled)
			{
				var exceptionModel = new ViewErrorModel
				{
					Url = Request.Url.ToString(),
					Message = filterContext.Exception.Message,
					Exception = filterContext.Exception
				};

				if (filterContext.Exception is NotAuthorizedException)
				{
					// log access failure?
				}

				filterContext.Result = View("Error", exceptionModel);
				filterContext.ExceptionHandled = true;
				return;
			}
			else
			{
				return;
			}
		}


		//protected void SetUserState(UserState state)
		//{
		//	context.SetUserState(state);
		//}

		//protected UserState GetUserState()
		//{
		//	var state = context.GetUserState();

		//	return state ?? UserState.Anonymous;
		//}

		protected User GetCurrentUser()
		{
			if (User.Identity.IsAuthenticated)
			{
				return dataContext.Users.First(u => u.UserName == User.Identity.Name);
			}
			else
			{
				return null;
			}
		}

		protected ISiteLocalizer GetLocalizer()
		{
            return new SiteLocalizerFactory().GetXmlStringSiteLocalizer(siteSettings.Language.BaseCultureCode, siteSettings.Language.LanguageFilesDirectory);
		}

		private void ReturnString(string s, int i)
		{
		}

		[DebuggerStepThrough]
		protected void RequireLogin()
		{
			if (GetCurrentUser() != null)
			{
				throw new LoginRequiredException();
			}
		}

		[DebuggerStepThrough]
		protected void RequireAdmin()
		{
			RequireLogin();

			if (GetCurrentUser() != null && GetCurrentUser().IsAdmin)
			{
				throw new NotAuthorizedException("You must be an administrator");
			}
		}

		protected string GetRequestUrlBase()
		{
			return string.Format("{0}://{1}",
				Request.Url.GetComponents(UriComponents.Scheme, UriFormat.Unescaped),
				Request.Url.GetComponents(UriComponents.HostAndPort, UriFormat.Unescaped));
		}
	}
}