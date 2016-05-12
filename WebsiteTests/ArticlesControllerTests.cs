using mbsoft.BrewClub.Data;
using mbsoft.BrewClub.Website;
using mbsoft.BrewClub.Website.Controllers;
using mbsoft.BrewClub.Website.Models.Articles;
using mbsoft.BrewClub.Website.Settings;
using Moq;
using System.Web.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Net;
using System;

namespace WebsiteTests
{
    public class ArticlesControllerTests
    {
        [Fact]
        public void Index_ReturnsView()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            modelConverter.Setup(x => x.ConvertToArticlesViewModel(It.IsAny<IEnumerable<Article>>())).Returns(new ArticlesViewModel());

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Articles();

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreateGet_ReturnsView()
        {
            var dbContext = new Mock<BrewClubDbContext>();
            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Create();

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreatePost_InvalidModel_ReturnsView()
        {
            var dbContext = new Mock<BrewClubDbContext>();
            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var model = new ArticleCreateViewModel();
            
            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            target.ModelState.AddModelError("test", "test");

            var actual = target.Create(model);

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreatePost_ValidModel_ReturnsRedirect()
        {
            var userProfilesDbSet = new Mock<System.Data.Entity.DbSet<User>>();
            var dummyUser = new User() { Id = Guid.NewGuid().ToString(), UserName = "test", Email = "test@gmail.com", FullName = "Test Person" };
            userProfilesDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns(dummyUser);

            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Users).Returns(userProfilesDbSet.Object);
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            int articleID = 2;
            modelConverter.Setup(x => x.ConvertArticleCreateViewModelToDataArticle(It.IsAny<ArticleCreateViewModel>(), It.IsAny<User>(), It.IsAny<System.DateTime>())).Returns(new Article() { PostedItemID = articleID });

            var model = new ArticleCreateViewModel();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            var actual = target.Create(model);

            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.Equal("", ((RedirectToRouteResult)actual).RouteName);
            Assert.Equal(articleID, ((RedirectToRouteResult)actual).RouteValues["articleID"]);
        }

        [Fact]
        public void EditGet_ValidArticleID_ReturnsView()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Article());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Edit(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void EditGet_InvalidArticleID_ReturnsHttpNotFound()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<Article>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Edit(1);

            Assert.NotNull(actual);
            Assert.IsType<HttpNotFoundResult>(actual);
        }

        [Fact]
        public void EditPost_InvalidArticleID_ReturnsHttpStatusCodeBadRequest()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<Article>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var model = new ArticleEditViewModel() { ArticleID = 1 };
            var actual = target.Edit(model);

            Assert.NotNull(actual);
            Assert.IsType<HttpStatusCodeResult>(actual);
            Assert.Equal(((HttpStatusCodeResult)actual).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void EditPost_InvalidModel_ReturnsView()
        {
            var dbContext = new Mock<BrewClubDbContext>();
            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var model = new ArticleEditViewModel();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            target.ModelState.AddModelError("test", "test");

            var actual = target.Edit(model);

            Assert.NotNull(actual);
        }

        [Fact]
        public void EditPost_ValidModel_ReturnsRedirect()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Article());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            modelConverter.Setup(x => x.ConvertArticleEditViewModelToDataArticle(It.IsAny<ArticleEditViewModel>(), It.IsAny<System.DateTime>(), It.IsAny<Article>()));

            int articleID = 2;
            var model = new ArticleEditViewModel() { ArticleID = articleID };

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            var actual = target.Edit(model);

            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.Equal("", ((RedirectToRouteResult)actual).RouteName);
            Assert.Equal(articleID, ((RedirectToRouteResult)actual).RouteValues["articleID"]);
        }

        [Fact]
        public void DeleteGet_InvalidArticleID_ReturnsHttpNotFound()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<Article>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Delete(1);

            Assert.NotNull(actual);
            Assert.IsType<HttpNotFoundResult>(actual);
        }

        [Fact]
        public void DeleteGet_ValidArticleID_ReturnsView()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Article());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Delete(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void DeleteConfirmedPost_InvalidArticleID_ReturnsHttpStatusCodeBadRequest()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<Article>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.DeleteConfirmed(1);

            Assert.NotNull(actual);
            Assert.IsType<HttpStatusCodeResult>(actual);
            Assert.Equal(((HttpStatusCodeResult)actual).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteConfirmedPost_ReturnsRedirect()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Article());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            var actual = target.DeleteConfirmed(1);

            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.Equal("", ((RedirectToRouteResult)actual).RouteName);
        }

        [Fact]
        public void Details_InvalidArticleID_ReturnsHttpNotFound()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<Article>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Details(1);

            Assert.NotNull(actual);
            Assert.IsType<HttpNotFoundResult>(actual);
        }

        [Fact]
        public void Details_ValidArticleID_ReturnsView()
        {
            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Article());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            modelConverter.Setup(x => x.ConvertToArticleDetailsViewModel(It.IsAny<Article>())).Returns(new ArticleDetailsViewModel());

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.Details(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreateCommentGet_ReturnsView()
        {
            var dbContext = new Mock<BrewClubDbContext>();
            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.CreateComment(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreateCommentPost_InvalidModel_ReturnsView()
        {
            var dbContext = new Mock<BrewClubDbContext>();
            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var model = new CreateCommentViewModel();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            target.ModelState.AddModelError("test", "test");

            var actual = target.CreateComment(model);

            Assert.NotNull(actual);
        }

        [Fact]
        public void CreateCommentPost_ValidModel_ReturnsRedirect()
        {
            var userProfilesDbSet = new Mock<System.Data.Entity.DbSet<User>>();
            var dummyUser = new User() { Id = Guid.NewGuid().ToString(), UserName = "test", Email = "test@gmail.com", FullName = "Test Person" };
            userProfilesDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns(dummyUser);

            var articlesDbSet = new Mock<System.Data.Entity.DbSet<Article>>();
            articlesDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns(new Article() { Comments = new List<ArticleComment>() });

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.Users).Returns(userProfilesDbSet.Object);
            dbContext.Setup(x => x.Articles).Returns(articlesDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            int articleID = 2;
            modelConverter.Setup(x => x.ConvertCreateCommentViewModelToDataComment(It.IsAny<CreateCommentViewModel>(), It.IsAny<User>(), It.IsAny<System.DateTime>())).Returns(new ArticleComment());

            var model = new CreateCommentViewModel() { ArticleID = articleID };

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            var actual = target.CreateComment(model);

            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.Equal("", ((RedirectToRouteResult)actual).RouteName);
            Assert.Equal(articleID, ((RedirectToRouteResult)actual).RouteValues["articleID"]);
        }

        [Fact]
        public void EditCommentGet_InvalidArticleCommentID_ReturnsHttpNotFound()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<ArticleComment>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.EditComment(1);

            Assert.NotNull(actual);
            Assert.IsType<HttpNotFoundResult>(actual);
        }

        [Fact]
        public void EditCommentGet_ValidArticleCommentID_ReturnsView()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<ArticleComment>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();
            modelConverter.Setup(x => x.ConvertToEditCommentViewModel(It.IsAny<ArticleComment>())).Returns(new EditCommentViewModel());

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.EditComment(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void EditCommentPost_InvalidArticleCommentID_ReturnsHttpStatusCodeBadRequest()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<ArticleComment>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var model = new EditCommentViewModel() { CommentID = 1 };
            var actual = target.EditComment(model);

            Assert.NotNull(actual);
            Assert.IsType<HttpStatusCodeResult>(actual);
            Assert.Equal(((HttpStatusCodeResult)actual).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void EditCommentPost_InvalidModel_ReturnsView()
        {
            var dbContext = new Mock<BrewClubDbContext>();
            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var model = new EditCommentViewModel();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            target.ModelState.AddModelError("test", "test");

            var actual = target.EditComment(model);

            Assert.NotNull(actual);
        }

        [Fact]
        public void EditCommentPost_ValidModel_ReturnsRedirect()
        {
            int postedItemID = 1;
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            var existingComment = new ArticleComment() { PostedItemID = postedItemID };
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(existingComment);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            modelConverter.Setup(x => x.ConvertEditCommentViewModelToDataComent(It.IsAny<EditCommentViewModel>(), It.IsAny<System.DateTime>(), It.IsAny<ArticleComment>()));

            var model = new EditCommentViewModel() { CommentID = 2 };

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            var actual = target.EditComment(model);

            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.Equal("", ((RedirectToRouteResult)actual).RouteName);
            Assert.Equal(postedItemID, ((RedirectToRouteResult)actual).RouteValues["articleID"]);
        }

        [Fact]
        public void DeleteCommentGet_InvalidCommentID_ReturnsHttpNotFound()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<ArticleComment>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.DeleteComment(1);

            Assert.NotNull(actual);
            Assert.IsType<HttpNotFoundResult>(actual);
        }

        [Fact]
        public void DeleteCommentGet_ValidCommentID_ReturnsView()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new ArticleComment());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();

            var modelConverter = new Mock<IArticleViewModelConverter>();
            modelConverter.Setup(x => x.ConvertToDeleteCommentViewModel(It.IsAny<ArticleComment>()));

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.DeleteComment(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void DeleteCommentConfirmedGet_InvalidCommentID_ReturnsHttpStatusCodeBadRequest()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns<ArticleComment>(null);

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);

            var actual = target.DeleteCommentConfirmed(1, 1);

            Assert.NotNull(actual);
            Assert.IsType<HttpStatusCodeResult>(actual);
            Assert.Equal(((HttpStatusCodeResult)actual).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteCommentConfirmedPost_ReturnsRedirect()
        {
            var articleCommentsDbSet = new Mock<System.Data.Entity.DbSet<ArticleComment>>();
            articleCommentsDbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new ArticleComment());

            var dbContext = new Mock<BrewClubDbContext>();
            dbContext.Setup(x => x.ArticleComments).Returns(articleCommentsDbSet.Object);

            var userContext = new Mock<IUserContext>();
            var siteSettings = new Mock<ISiteSettings>();
            var modelConverter = new Mock<IArticleViewModelConverter>();

            var target = new ArticlesController(dbContext.Object, userContext.Object, siteSettings.Object, modelConverter.Object);
            int articleID = 2;
            var actual = target.DeleteCommentConfirmed(1, articleID);

            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.Equal("", ((RedirectToRouteResult)actual).RouteName);
            Assert.Equal(articleID, ((RedirectToRouteResult)actual).RouteValues["articleID"]);
        }
    }
}
