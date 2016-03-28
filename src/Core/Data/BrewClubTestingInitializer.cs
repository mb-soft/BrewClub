using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbsoft.BrewClub.Data
{
	public class BrewClubTestingInitializer : System.Data.Entity.DropCreateDatabaseAlways<BrewClubContext>
	{

		protected override void Seed(BrewClubContext context)
		{
			// this is pretty bad-ass - EF will recognize when the database needs to be changed, and this initializer is used to make it add/drop
			// in this method, we can seed it with test data.

			var mikesProfile = new UserProfile
			{
				UserProfileID = 1,
                DisplayName = "Mike",
                Email = "mike@Mike.com",
			};

            context.UserProfiles.Add(mikesProfile);

            var articleComments = new List<ArticleComment>
            {
                new ArticleComment { ArticleCommentID = 1, Author = mikesProfile, Body = "killer post man", DateCreated = DateTime.Now },
                new ArticleComment { ArticleCommentID = 2, Author = mikesProfile, Body = "pretty weak dude", DateCreated = DateTime.Now }
            };

            var articles = new List<Article>
            {
                new Article { Author = mikesProfile, Body = "This is a test article", DateCreated = DateTime.Now, Title = "The first test", Comments = articleComments },
			};

			articles.ForEach(a => context.Articles.Add(a));
			context.SaveChanges();

		}

	}
}
