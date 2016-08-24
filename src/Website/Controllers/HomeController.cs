using System.Web.Mvc;

namespace mbsoft.BrewClub.Website.Controllers
{

    public class HomeController : ControllerBase
	{

		public HomeController(Data.BrewClubDbContext dataContext, Settings.ISiteSettings siteSettings)
            : base(dataContext, siteSettings)
        {

		}


        [Route("index")]
        [Route("")]
        public ActionResult Index()
		{
			return View();
		}

	}
}