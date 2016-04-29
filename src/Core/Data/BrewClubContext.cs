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

        public virtual DbSet<PostedItem> PostedItems { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Classified> Classifieds { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<ArticleComment> ArticleComments { get; set; }

    }

}