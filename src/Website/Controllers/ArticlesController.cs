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
using mbsoft.BrewClub.Website.Settings;

namespace mbsoft.BrewClub.Website.Controllers
{
    [RoutePrefix("articles")]
    [Route("{action}")]
    public class ArticlesController : ControllerBase
    {
        private IArticleViewModelConverter articleModelConverter;

        public ArticlesController(BrewClubContext dataContext, IUserContext context, ISiteSettings siteSettings, IArticleViewModelConverter articleModelConverter)
			: base(dataContext, context, siteSettings)
		{
            this.articleModelConverter = articleModelConverter;
        }

        [Route("")]
        public ActionResult Articles()
        {
            var model = articleModelConverter.ConvertToArticlesViewModel(dataContext.Articles);
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
                dataContext.Articles.Add(convertedDataArticle);
                dataContext.SaveChanges();

                return RedirectToAction("details", new { articleID = convertedDataArticle.PostedItemID });
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
            var article = dataContext.Articles.Find(articleID);
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

        [HttpGet]
        public ActionResult CreateComment(int articleID)
        {
            var model = new CreateCommentViewModel() { ArticleID = articleID };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CreateCommentViewModel comment)
        {
            if (ModelState.IsValid == true)
            {
                var user = GetDummyUser();

                var convertedDataArticleComment = articleModelConverter.ConvertCreateCommentViewModelToDataArticle(comment, user, DateTime.Now);
                dataContext.Articles.Find(comment.ArticleID).Comments.Add(convertedDataArticleComment);
                dataContext.SaveChanges();

                return RedirectToAction("details", new { articleID = comment.ArticleID });
            }
            else
            {
                return View(comment);
            }
        }

    }
}
