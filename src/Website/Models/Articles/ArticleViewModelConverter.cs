using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class ArticleViewModelConverter : IArticleViewModelConverter
    {
        public ArticleViewModelConverter()
        {

        }

        public ArticlesViewModel ConvertToArticlesViewModel(IEnumerable<BrewClub.Data.Article> dataArticles)
        {
            var convertedArticles = new ArticlesViewModel();

            foreach (var dataArticle in dataArticles)
            {
                convertedArticles.ArticleListItems.Add(ConvertToArticlesViewModelItem(dataArticle));
            }

            return convertedArticles;
        }

        public ArticlesViewModelListItem ConvertToArticlesViewModelItem(BrewClub.Data.Article dataArticle)
        {
            return new ArticlesViewModelListItem()
            {
                ArticleId = dataArticle.PostedItemId,
                AuthorID = dataArticle.Author.UserProfileId,
                AuthorName = dataArticle.Author.DisplayName,
                CommentCount = dataArticle.Comments.Count(),
                LastActivity = dataArticle.GetLastActivity(),
                Title = dataArticle.Title,
            };

        }

        public Data.Article ConvertArticleCreateViewModelToDataArticle(ArticleCreateViewModel model, BrewClub.Data.UserProfile author, DateTime dateCreated)
        {
            return new Data.Article()
            {
                Author = author,
                Body = model.Body,
                DateCreated = dateCreated,
                Title = model.Title,
            };
        }

        public ArticleDetailsViewModel ConvertToArticleDetailsViewModel(Data.Article dataArticle)
        {
            return new ArticleDetailsViewModel()
            {
                Body = dataArticle.Body,
                Comments = dataArticle.Comments,
                DateCreated = dataArticle.DateCreated,
                DateLastEdited = dataArticle.LastEdit,
                ArticleID = dataArticle.PostedItemId,
                Title = dataArticle.Title,
            };
        }


    }
}