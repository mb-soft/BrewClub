using System.Collections.Generic;
using mbsoft.BrewClub.Data;
using System;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public interface IArticleViewModelConverter
    {
        ArticlesViewModelListItem ConvertToArticlesViewModelItem(Article dataArticle);
        ArticlesViewModel ConvertToArticlesViewModel(IEnumerable<Article> dataArticles);
        Article ConvertArticleCreateViewModelToDataArticle(ArticleCreateViewModel model, User author, DateTime dateCreated);
        ArticleDetailsViewModel ConvertToArticleDetailsViewModel(Data.Article dataArticle);
        ICollection<CommentDetailsViewModel> ConvertToArticleDetailsViewModelComment(IEnumerable<Data.ArticleComment> dataComments);
        CommentDetailsViewModel ConvertToArticleDetailsViewModelComment(Data.ArticleComment dataComment);
        EditCommentViewModel ConvertToEditCommentViewModel(Data.ArticleComment dataComment);
        void ConvertEditCommentViewModelToDataComent(EditCommentViewModel model, DateTime dateEdited, ArticleComment commentToUpdate);
        Data.ArticleComment ConvertCreateCommentViewModelToDataComment(CreateCommentViewModel model, User author, DateTime dateCreated);
        void ConvertArticleEditViewModelToDataArticle(ArticleEditViewModel model, DateTime dateEdited, Data.Article articleToUpdate);
        ArticleEditViewModel ConvertToArticleEditViewModel(Data.Article dataArticle);
        ArticleDeleteViewModel ConvertToArticleDeleteViewModel(Data.Article dataArticle);
        DeleteCommentViewModel ConvertToDeleteCommentViewModel(Data.ArticleComment dataComment);
    }
}