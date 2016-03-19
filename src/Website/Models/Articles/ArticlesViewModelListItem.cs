using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class ArticlesViewModelListItem
    {
        public ArticlesViewModelListItem()
        {

        }

        public int ArticleId { get; set; }

        public int AuthorID { get; set; }

        public string AuthorName { get; set; }

        public string Title { get; set; }

        public DateTime LastActivity { get; set; }

        public int CommentCount { get; set; }
    }
}