using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class Article : PostedItem
	{
        [Required]
        public string Body { get; set; }

        public string Url { get; set; }

        public virtual ICollection<PostedItemComment> Comments { get; set; }

	}
}
