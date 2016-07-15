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

        public ArticlesController(BrewClubDbContext dataContext, ISiteSettings siteSettings, IArticleViewModelConverter articleModelConverter)
			: base(dataContext, siteSettings)
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleCreateViewModel article)
        {
            if (ModelState.IsValid == true)
            {
                var user = GetCurrentUser();

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

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int articleID)
        {
            var article = dataContext.Articles.Find(articleID);
            if (article == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = articleModelConverter.ConvertToArticleEditViewModel(article);
                return View(model);
            }

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleEditViewModel article)
        {
            if (ModelState.IsValid == true)
            {
                var existingArticle = dataContext.Articles.Find(article.ArticleID);
                if (existingArticle == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    articleModelConverter.ConvertArticleEditViewModelToDataArticle(article, DateTime.Now, existingArticle);
                    dataContext.SaveChanges();

                    return RedirectToAction("details", new { articleID = article.ArticleID });
                }
            }
            else
            {
                return View(article);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int articleID)
        {
            var article = dataContext.Articles.Find(articleID);
            if (article == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = articleModelConverter.ConvertToArticleDeleteViewModel(article);
                return View(model);
            }

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int articleID)
        {
            var existingArticle = dataContext.Articles.Find(articleID);
            if (existingArticle == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                dataContext.Articles.Remove(existingArticle);
                dataContext.SaveChanges();

                return RedirectToAction("articles");
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
        [Authorize]
        public ActionResult CreateComment(int articleID)
        {
            var model = new CreateCommentViewModel() { ArticleID = articleID };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(CreateCommentViewModel comment)
        {
            if (ModelState.IsValid == true)
            {
                var user = GetCurrentUser();

                var convertedDataArticleComment = articleModelConverter.ConvertCreateCommentViewModelToDataComment(comment, user, DateTime.Now);
                dataContext.Articles.Find(comment.ArticleID).Comments.Add(convertedDataArticleComment);
                dataContext.SaveChanges();

                return RedirectToAction("details", new { articleID = comment.ArticleID });
            }
            else
            {
                return View(comment);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditComment(int commentID)
        {
            var comment = dataContext.ArticleComments.Find(commentID);
            if (comment == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = articleModelConverter.ConvertToEditCommentViewModel(comment);
                return View(model);
            }

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(EditCommentViewModel comment)
        {
            if (ModelState.IsValid == true)
            {
                var existingComment = dataContext.ArticleComments.Find(comment.CommentID);
                if (existingComment == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    articleModelConverter.ConvertEditCommentViewModelToDataComent(comment, DateTime.Now, existingComment);
                    dataContext.SaveChanges(); 

                    return RedirectToAction("details", new { articleID = existingComment.PostedItemID });
                }
            }
            else
            {
                return View(comment);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteComment(int commentID)
        {
            var comment = dataContext.ArticleComments.Find(commentID);
            if (comment == null)
            {
                return HttpNotFound();
            }
            else
            {
                var model = articleModelConverter.ConvertToDeleteCommentViewModel(comment);
                return View(model);
            }

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int commentID, int articleID)
        {
            var existingComment = dataContext.ArticleComments.Find(commentID);
            if (existingComment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                dataContext.ArticleComments.Remove(existingComment);
                dataContext.SaveChanges();

                return RedirectToAction("details", new { articleID = articleID });
            }
        }

    }
}
