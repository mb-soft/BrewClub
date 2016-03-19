using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class BrewClubTestingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BrewClubContext>
	{

		protected override void Seed(BrewClubContext context)
		{
			// this is pretty bad-ass - EF will recognize when the database needs to be changed, and this initializer is used to make it add/drop
			// in this method, we can seed it with test data.

			var mikesProfile = new UserProfile
			{
				UserProfileId = 1,
                DisplayName = "Mike",
                Email = "mike@Mike.com",
			};

            context.UserProfiles.Add(mikesProfile);

			var articles = new List<Article>
			{
				new Article { Author = mikesProfile, Body = "This is a test article", DateCreated = DateTime.Now, Title = "The first test" },
			};

			articles.ForEach(a => context.Articles.Add(a));
			context.SaveChanges();


		}

	}
}
