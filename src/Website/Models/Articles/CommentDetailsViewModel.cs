using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class CommentDetailsViewModel
    {
        public int ArticleCommentID { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleCommentAuthorNameLabel)]
        public string AuthorName { get; set; }

        [DataType(DataType.MultilineText)]
        [LocalizedDisplayName(LocalizedStringKeys.ArticleCommentBodyLabel)]
        public string Body { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleCommentDateCreatedLabel)]
        public DateTime DateCreated { get; set; }
    }
}