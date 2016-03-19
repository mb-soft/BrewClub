using System.Collections.Generic;
using mbsoft.BrewClub.Data;
using System;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public interface IArticleViewModelConverter
    {
        ArticlesViewModelListItem ConvertToArticlesViewModelItem(Article dataArticle);
        ArticlesViewModel ConvertToArticlesViewModel(IEnumerable<Article> dataArticles);
        Article ConvertArticleCreateViewModelToDataArticle(ArticleCreateViewModel model, UserProfile author, DateTime dateCreated);
        ArticleDetailsViewModel ConvertToArticleDetailsViewModel(Data.Article dataArticle);
    }
}