using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class ArticleComment
	{

		public int ArticleCommentId { get; set; }

		public User Author { get; set; }

		public string Body { get; set; }

	}
}
