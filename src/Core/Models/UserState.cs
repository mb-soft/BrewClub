//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using mbsoft.BrewClub.Data;

//namespace mbsoft.BrewClub.Models
//{
//	[Serializable]
//	public class UserState
//	{

//		public string Email { get; private set; }
		
//		public bool IsAdmin { get; private set; }

//		public bool IsModerator { get; private set; }

//		public bool IsMember { get; private set; }

//		public bool IsUser { get; private set; }

//		public bool IsAnonymous { get; private set; }

//		public string DisplayName { get; private set; }


//		private UserState()
//		{
//		}

//		public static UserState Create(User user)
//		{
//			return new UserState
//			{
//				DisplayName = user.DisplayName,
//				IsAdmin = user.IsAdmin,
//				IsModerator = user.IsModerator,
//				IsMember = user.IsMember,
//				IsUser = user.IsUser,
//				Email = user.Email,
//				IsAnonymous = false,
//			};
//		}

//		public static UserState Anonymous
//		{
//			get
//			{
//				return new UserState
//				{
//					DisplayName = "",
//					IsAdmin = false,
//					IsModerator = false,
//					IsMember = false,
//					IsUser = false,
//					Email = "",
//					IsAnonymous = true,
//				};
//			}
//		}

//	}
//}
