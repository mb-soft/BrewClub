using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using mbsoft.BrewClub.Data;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class ArticleDetailsViewModel
    {
        public int ArticleID { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleAuthorNameLabel)]
        public string AuthorName { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleTitleLabel)]
        public string Title { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleBodyLabel)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleDateCreatedLabel)]
        public DateTime DateCreated { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleDateLastEditedLabel)]
        public DateTime? DateLastEdited { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleCommentsLabel)]
        public ICollection<CommentDetailsViewModel> Comments { get; set; } = new List<CommentDetailsViewModel>();
    }
}