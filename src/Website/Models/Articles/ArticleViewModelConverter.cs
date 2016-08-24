using mbsoft.BrewClub.Authorization;
using mbsoft.BrewClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class ArticleViewModelConverter : IArticleViewModelConverter
    {
        private IPostedItemAuthorizer postedItemAuthorizer;

        public ArticleViewModelConverter(IPostedItemAuthorizer postedItemAuthorizer)
        {
            this.postedItemAuthorizer = postedItemAuthorizer;
        }

        public ArticlesViewModel ConvertToArticlesViewModel(IEnumerable<Article> dataArticles, IEnumerable<string> currentUserRoleIDs)
        {
            var convertedArticles = new ArticlesViewModel()
            {
                IsCreateArticleAuthorized = postedItemAuthorizer.IsAllowedToCreatePost(currentUserRoleIDs),
            };

            foreach (var dataArticle in dataArticles)
            {
                convertedArticles.ArticleListItems.Add(ConvertToArticlesViewModelItem(dataArticle));
            }

            return convertedArticles;
        }

        public ArticlesViewModelListItem ConvertToArticlesViewModelItem(Article dataArticle)
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
                Url = model.Url,
            };
        }

        public Data.PostedItemComment ConvertCreateCommentViewModelToDataComment(CreateCommentViewModel model, User author, DateTime dateCreated)
        {
            return new Data.PostedItemComment()
            {
                Author = author,
                Body = model.Body,
                DateCreated = dateCreated,
            };
        }

        public ArticleDetailsViewModel ConvertToArticleDetailsViewModel(Article dataArticle, IEnumerable<string> currentUserRoleIDs, string currentUserID)
        {
            return new ArticleDetailsViewModel()
            {
                AuthorName = dataArticle.Author.FullName,
                Body = dataArticle.Body,
                Comments = ConvertToArticleDetailsViewModelCommentCollection(dataArticle.Comments, currentUserRoleIDs, currentUserID),
                DateCreated = dataArticle.DateCreated,
                DateLastEdited = dataArticle.LastEdit,
                ArticleID = dataArticle.PostedItemID,
                Title = dataArticle.Title,
                IsDeleteAuthorized = postedItemAuthorizer.IsPostedItemDeletable(currentUserID, currentUserRoleIDs, dataArticle),
                IsEditAuthorized = postedItemAuthorizer.IsPostedItemEditable(currentUserID, currentUserRoleIDs, dataArticle),
                IsCreateCommentAuthorized = postedItemAuthorizer.IsAllowedToCreatPostComment(currentUserRoleIDs),
                Url = dataArticle.Url,
            };
        }

        public ICollection<CommentDetailsViewModel> ConvertToArticleDetailsViewModelCommentCollection(IEnumerable<Data.PostedItemComment> dataComments, IEnumerable<string> currentUserRoleIDs, string currentUserID)
        {
            var convertedComments = new List<CommentDetailsViewModel>();

            foreach (var comment in dataComments)
            {
                convertedComments.Add(ConvertToArticleDetailsViewModelComment(comment, currentUserRoleIDs, currentUserID));
            }

            return convertedComments;
        }

        public CommentDetailsViewModel ConvertToArticleDetailsViewModelComment(PostedItemComment dataComment, IEnumerable<string> currentUserRoleIDs, string currentUserID)
        {
            return new CommentDetailsViewModel()
            {
                ArticleCommentID = dataComment.PostedItemCommentID,
                AuthorName = dataComment.Author.FullName,
                Body = dataComment.Body,
                DateCreated = dataComment.DateCreated,
                IsDeleteAuthorized = postedItemAuthorizer.IsPostedItemCommentDeletable(currentUserID, currentUserRoleIDs, dataComment),
                IsEditAuthorized = postedItemAuthorizer.IsPostedItemCommentEditable(currentUserID, currentUserRoleIDs, dataComment),
            };

        }

        public ArticleEditViewModel ConvertToArticleEditViewModel(Data.Article dataArticle)
        {
            return new ArticleEditViewModel()
            {
                ArticleID = dataArticle.PostedItemID,
                Body = dataArticle.Body,
                Title = dataArticle.Title,
                Url = dataArticle.Url,
            };
        }

        public void ConvertArticleEditViewModelToDataArticle(ArticleEditViewModel model, DateTime dateEdited, Data.Article articleToUpdate)
        {
            articleToUpdate.Body = model.Body;
            articleToUpdate.Title = model.Title;
            articleToUpdate.PostedItemID = model.ArticleID;
            articleToUpdate.LastEdit = dateEdited;
            articleToUpdate.Url = model.Url;
        }

        public EditCommentViewModel ConvertToEditCommentViewModel(PostedItemComment dataComment)
        {
            return new EditCommentViewModel()
            {
                Body = dataComment.Body,
                CommentID = dataComment.PostedItemCommentID,
            };
        }

        public void ConvertEditCommentViewModelToDataComent(EditCommentViewModel model, DateTime dateEdited, PostedItemComment commentToUpdate)
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

        public DeleteCommentViewModel ConvertToDeleteCommentViewModel(PostedItemComment dataComment)
        {
            return new DeleteCommentViewModel()
            {
                ArticleCommentID = dataComment.PostedItemCommentID,
                ArticleID = dataComment.PostedItemID,
                AuthorName = dataComment.Author.FullName,
                Body = dataComment.Body,
                DateCreated = dataComment.DateCreated,
            };
        }
    }
}