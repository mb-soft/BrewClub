using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class Article : PostedItem
	{

		public string Body { get; set; }

		public virtual ICollection<ArticleComment> Comments { get; set; }

	}
}
