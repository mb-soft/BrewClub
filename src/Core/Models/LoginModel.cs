﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Models
{
	public class LoginModel
	{

		public string UsernameEmail { get; set; }

		public string Password { get; set; }

		public bool RememberMe { get; set; }

	}
}