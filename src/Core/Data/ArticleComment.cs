using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class ArticleComment
	{

		public int ArticleCommentID { get; set; }

        public int PostedItemID { get; set; }

        public virtual UserProfile Author { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastEdit { get; set; }

        [Required]
        public string Body { get; set; }

	}
}
