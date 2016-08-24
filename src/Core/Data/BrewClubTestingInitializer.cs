using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mbsoft.BrewClub.Data
{
	public class BrewClubTestingInitializer : System.Data.Entity.DropCreateDatabaseAlways<BrewClubDbContext>
	{

        protected override void Seed(BrewClubDbContext context)
        {
            // this is pretty bad-ass - EF will recognize when the database needs to be changed, and this initializer is used to make it add/drop
            // in this method, we can seed it with test data.

            // set up the default roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var admin = new IdentityRole(BrewClubRoleManager.Admin) { Id = BrewClubRoleManager.Admin };
            var moderator = new IdentityRole(BrewClubRoleManager.Moderator) { Id = BrewClubRoleManager.Moderator };
            var member = new IdentityRole(BrewClubRoleManager.Member) { Id = BrewClubRoleManager.Member };
            var user = new IdentityRole(BrewClubRoleManager.User) { Id = BrewClubRoleManager.User };

            roleManager.Create(admin);
            roleManager.Create(moderator);
            roleManager.Create(member);
            roleManager.Create(user);

            var userManager = new UserManager<User>(new UserStore<User>(context));

            var mike = new User { Id = Guid.NewGuid().ToString(), UserName = "miketest", Email = "pudds55+test@gmail.com", FullName = "Mike Dryden" };
            userManager.Create(mike, "mike123");
            // var brad
            // userManager.Create(brad, "");

            // add users to roles
            userManager.AddToRole(mike.Id, admin.Name);
            // userMamager.AddToRole(brad.Id, admin.Name);

            var articleComments = new List<PostedItemComment>
            {
                new PostedItemComment { PostedItemCommentID = 1, Author = mike, Body = "killer post man", DateCreated = DateTime.Now },
                new PostedItemComment { PostedItemCommentID = 2, Author = mike, Body = "pretty weak dude", DateCreated = DateTime.Now }
            };

            var articles = new List<Article>
            {
                new Article { Author = mike, Body = "This is a test article", DateCreated = DateTime.Now, Title = "The first test", Comments = articleComments }
            };

            articles.ForEach(a => context.Articles.Add(a));
            context.SaveChanges();
        }
	}
}
