using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Data;

namespace mbsoft.BrewClub.Website.Models.Account
{
	public class EditRegistration
	{
		
		[Required]
		public string UserName { get; set; }

		[Required]
		public string EmailAddress { get; set; }

		[Required]
		public string Password { get; set; }
	}
}