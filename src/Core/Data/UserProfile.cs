using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class UserProfile
	{
		public const int DisplayNameMaxLength = 50;

		public const int EmailMinLength = 5;
		public const int EmailMaxLength = 50;

		// I don't yet understand how membership and entity framework are supposed to integrate.
		public int UserId { get; set; }

		[Required]
		[MaxLength(DisplayNameMaxLength)]
        public string DisplayName { get; set; }

		[Required]
		[MinLength(EmailMinLength)]
		[MaxLength(EmailMaxLength)]
		public string Email { get; set; }

	}
}
