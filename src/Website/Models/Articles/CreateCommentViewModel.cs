using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class CreateCommentViewModel
    {
        public int ArticleID { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [LocalizedDisplayName(LocalizedStringKeys.ArticleCommentBodyLabel)]
        public string Body { get; set; }
    }
}