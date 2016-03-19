using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Website.Models.Articles;

namespace mbsoft.BrewClub.Website.Controllers
{
    [RoutePrefix("articles")]
    [Route("{action}")]
    public class ArticlesController : ControllerBase
    {
        private IArticleViewModelConverter articleModelConverter;

        public ArticlesController(IArticleViewModelConverter articleModelConverter)
			: base(GetDefaultUserContext(), GetDefaultSiteSettings())
		{
            this.articleModelConverter = articleModelConverter;
        }

        [Route("")]
        public ActionResult Articles()
        {
            var model = articleModelConverter.ConvertToArticlesViewModel(GetContext().Articles);
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleCreateViewModel article)
        {
            if (ModelState.IsValid == true)
            {
                var user = GetDummyUser();

                var convertedDataArticle = articleModelConverter.ConvertArticleCreateViewModelToDataArticle(article, user, DateTime.Now);
                var dbContext = GetContext();
                dbContext.Articles.Add(convertedDataArticle);
                dbContext.SaveChanges();

                return RedirectToAction("details", new { articleID = convertedDataArticle.PostedItemId });
            }
            else
            {
                return View(article);
            }
        }

        [Route("{articleID:int}")]
        [Route("details/{articleID:int}")]
        public ActionResult Details(int articleID)
        {
            var article = GetContext().Articles.Find(articleID);
            if (article == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = articleModelConverter.ConvertToArticleDetailsViewModel(article);
                return View(model);
            }            
        }

    }
}
