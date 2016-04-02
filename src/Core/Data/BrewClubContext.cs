namespace mbsoft.BrewClub.Data
{
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class BrewClubContext : DbContext
    {
		public BrewClubContext()
			: base("name=BrewClubContext")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PostedItem> PostedItems { get; set; } 
		public DbSet<Article> Articles { get; set; }
		public DbSet<Classified> Classifieds { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }

    }

}