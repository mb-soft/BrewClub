﻿using mbsoft.BrewClub.Data;
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
                AuthorID = dataArticle.Author.UserProfileID,
                AuthorName = dataArticle.Author.DisplayName,
                CommentCount = dataArticle.Comments.Count(),
                LastActivity = dataArticle.GetLastActivity(),
                Title = dataArticle.Title,
            };

        }

        public Data.Article ConvertArticleCreateViewModelToDataArticle(ArticleCreateViewModel model, UserProfile author, DateTime dateCreated)
        {
            return new Data.Article()
            {
                Author = author,
                Body = model.Body,
                DateCreated = dateCreated,
                Title = model.Title,
            };
        }

        public Data.ArticleComment ConvertCreateCommentViewModelToDataArticle(CreateCommentViewModel model, UserProfile author, DateTime dateCreated)
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
                AuthorName = dataArticle.Author.DisplayName,
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
                ArticleCommentId = dataComment.ArticleCommentID,
                AuthorName = dataComment.Author.DisplayName,
                Body = dataComment.Body,
                DateCreated = dataComment.DateCreated,
            };

        }


    }
}