﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using mbsoft.BrewClub.Data;


namespace mbsoft.BrewClub.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //TODO: Remove this after we go live.
            Database.SetInitializer(new BrewClubTestingInitializer());

			using (var context = new BrewClubDbContext())
			{
				context.Database.Initialize(force: true);
			}

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
		
			
		}

    }
}
