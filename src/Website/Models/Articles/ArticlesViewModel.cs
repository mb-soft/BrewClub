using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class ArticlesViewModel 
    {
        public ICollection<ArticlesViewModelListItem> ArticleListItems { get; set; } = new List<ArticlesViewModelListItem>();
    }
}