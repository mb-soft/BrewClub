using mbsoft.BrewClub.Data;
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
                ArticleId = dataArticle.PostedItemID,
                AuthorID = dataArticle.Author.Id,
                AuthorName = dataArticle.Author.FullName,
                CommentCount = dataArticle.Comments.Count(),
                LastActivity = dataArticle.GetLastActivity(),
                Title = dataArticle.Title,
            };

        }

        public Data.Article ConvertArticleCreateViewModelToDataArticle(ArticleCreateViewModel model, User author, DateTime dateCreated)
        {
            return new Data.Article()
            {
                Author = author,
                Body = model.Body,
                DateCreated = dateCreated,
                Title = model.Title,
            };
        }

        public Data.ArticleComment ConvertCreateCommentViewModelToDataComment(CreateCommentViewModel model, User author, DateTime dateCreated)
        {
            return new Data.ArticleComment()
            {
                Author = author,
                Body = model.Body,
                DateCreated = dateCreated,
            };
        }

        public ArticleDetailsViewModel ConvertToArticleDetailsViewModel(Data.Article dataArticle)
        {
            return new ArticleDetailsViewModel()
            {
                AuthorName = dataArticle.Author.FullName,
                Body = dataArticle.Body,
                Comments = ConvertToArticleDetailsViewModelComment(dataArticle.Comments),
                DateCreated = dataArticle.DateCreated,
                DateLastEdited = dataArticle.LastEdit,
                ArticleID = dataArticle.PostedItemID,
                Title = dataArticle.Title,
            };
        }

        public ICollection<ArticleDetailsViewModelComment> ConvertToArticleDetailsViewModelComment(IEnumerable<Data.ArticleComment> dataComments)
        {
            var convertedComments = new List<ArticleDetailsViewModelComment>();

            foreach (var comment in dataComments)
            {
                convertedComments.Add(ConvertToArticleDetailsViewModelComment(comment));
            }

            return convertedComments;
        }

        public ArticleDetailsViewModelComment ConvertToArticleDetailsViewModelComment(Data.ArticleComment dataComment)
        {
            return new ArticleDetailsViewModelComment()
            {
                ArticleCommentID = dataComment.ArticleCommentID,
                AuthorName = dataComment.Author.FullName,
                Body = dataComment.Body,
                DateCreated = dataComment.DateCreated,
            };

        }

        public ArticleEditViewModel ConvertToArticleEditViewModel(Data.Article dataArticle)
        {
            return new ArticleEditViewModel()
            {
                ArticleID = dataArticle.PostedItemID,
                Body = dataArticle.Body,
                Title = dataArticle.Title,
            };
        }

        public void ConvertArticleEditViewModelToDataArticle(ArticleEditViewModel model, DateTime dateEdited, Data.Article articleToUpdate)
        {
            articleToUpdate.Body = model.Body;
            articleToUpdate.Title = model.Title;
            articleToUpdate.PostedItemID = model.ArticleID;
            articleToUpdate.LastEdit = dateEdited;
        }

        public EditCommentViewModel ConvertToEditCommentViewModel(ArticleComment dataComment)
        {
            return new EditCommentViewModel()
            {
                Body = dataComment.Body,
                CommentID = dataComment.ArticleCommentID,
            };
        }

        public void ConvertEditCommentViewModelToDataComent(EditCommentViewModel model, DateTime dateEdited, ArticleComment commentToUpdate)
        {
            commentToUpdate.Body = model.Body;
            commentToUpdate.LastEdit = dateEdited;
        }

        public ArticleDeleteViewModel ConvertToArticleDeleteViewModel(Article dataArticle)
        {
            return new ArticleDeleteViewModel()
            {
                ArticleID = dataArticle.PostedItemID,
                AuthorName = dataArticle.Author.FullName,
                Body = dataArticle.Body,
                DateCreated = dataArticle.DateCreated,
                DateLastEdited = dataArticle.LastEdit,
                Title = dataArticle.Title,
            };
        }

        public DeleteCommentViewModel ConvertToDeleteCommentViewModel(ArticleComment dataComment)
        {
            return new DeleteCommentViewModel()
            {
                ArticleCommentID = dataComment.ArticleCommentID,
                ArticleID = dataComment.PostedItemID,
                AuthorName = dataComment.Author.FullName,
                Body = dataComment.Body,
                DateCreated = dataComment.DateCreated,
            };
        }
    }
}