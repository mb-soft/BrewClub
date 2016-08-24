using System.ComponentModel.DataAnnotations;

namespace mbsoft.BrewClub.Website.Models.Articles
{
    public class ArticleCreateViewModel
    {
        [Required]
        [LocalizedDisplayName(LocalizedStringKeys.ArticleTitleLabel)]
        public string Title { get; set; }

        [Required]
        [LocalizedDisplayName(LocalizedStringKeys.ArticleBodyLabel)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [LocalizedDisplayName(LocalizedStringKeys.ArticleUrlLabel)]
        public string Url { get; set; }
    }
}