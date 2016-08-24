using System.Collections.Generic;
using mbsoft.BrewClub.Data;
using System;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public interface IArticleViewModelConverter
    {
        ArticlesViewModelListItem ConvertToArticlesViewModelItem(Article dataArticle);
        ArticlesViewModel ConvertToArticlesViewModel(IEnumerable<Article> dataArticles, IEnumerable<string> currentUserRoleIDs);
        Article ConvertArticleCreateViewModelToDataArticle(ArticleCreateViewModel model, User author, DateTime dateCreated);
        ArticleDetailsViewModel ConvertToArticleDetailsViewModel(Data.Article dataArticle, IEnumerable<string> currentUserRoleIDs, string currentUserID);
        ICollection<CommentDetailsViewModel> ConvertToArticleDetailsViewModelCommentCollection(IEnumerable<Data.PostedItemComment> dataComments, IEnumerable<string> currentUserRoleIDs, string currentUserID);
        CommentDetailsViewModel ConvertToArticleDetailsViewModelComment(Data.PostedItemComment dataComment, IEnumerable<string> currentUserRoleIDs, string currentUserID);
        EditCommentViewModel ConvertToEditCommentViewModel(Data.PostedItemComment dataComment);
        void ConvertEditCommentViewModelToDataComent(EditCommentViewModel model, DateTime dateEdited, PostedItemComment commentToUpdate);
        Data.PostedItemComment ConvertCreateCommentViewModelToDataComment(CreateCommentViewModel model, User author, DateTime dateCreated);
        void ConvertArticleEditViewModelToDataArticle(ArticleEditViewModel model, DateTime dateEdited, Data.Article articleToUpdate);
        ArticleEditViewModel ConvertToArticleEditViewModel(Data.Article dataArticle);
        ArticleDeleteViewModel ConvertToArticleDeleteViewModel(Data.Article dataArticle);
        DeleteCommentViewModel ConvertToDeleteCommentViewModel(Data.PostedItemComment dataComment);
    }
}