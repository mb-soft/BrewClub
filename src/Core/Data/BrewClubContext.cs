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
			modelBuilder.Entity<Article>().ToTable("Articles");
			
		}

		// public virtual DbSet<MyEntity> MyEntities { get; set; }


		public DbSet<PostedItem> PostedItems { get; set; } 
		public DbSet<Article> Articles { get; set; }
		public DbSet<Classified> Classifieds { get; set; }
		public DbSet<Recipe> Recipes { get; set; }

	}

}