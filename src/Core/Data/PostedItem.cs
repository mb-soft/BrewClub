using mbsoft.BrewClub.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class PostedItem : IAuthorizablePostedItem
	{
		public int PostedItemID { get; set; }

        //Can't use Required attribute here or we get lazy loading update error on save.
        //Required is defined via fluent API
        public virtual User Author { get; set; }

        public DateTime DateCreated { get; set; }

		public DateTime? LastEdit { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime GetLastActivity()
        {
            return (this.LastEdit.HasValue == true) ? this.LastEdit.Value : this.DateCreated;
        }

        string IAuthorizablePostedItem.PostedItemAuthorID
        {
            get
            {
                return Author.Id;
            }
        }
    }
}
