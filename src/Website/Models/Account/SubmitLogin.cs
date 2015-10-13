using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace mbsoft.BrewClub.Website.Models.Account
{
	public class SubmitLogin
	{

		[Required(ErrorMessage = "Username or email is required.")]
		public string UsernameEmail { get; set; }

		[Required(ErrorMessage = "Password field is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }

		[HiddenInput]
		public string ReturnUrl { get; set; }


	}
}
