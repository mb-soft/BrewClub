using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mbsoft.BrewClub.Data;

namespace mbsoft.BrewClub.Website.Models.Account
{
	public class SubmitRegistration
	{
		
		[Required(ErrorMessage = "Username is required.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email address is required.")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }
	}
}