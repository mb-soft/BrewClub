namespace mbsoft.BrewClub.Data
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using Microsoft.AspNet.Identity.EntityFramework;

	public class BrewClubDbContext : IdentityDbContext<User>
	{
		public BrewClubDbContext()
			: base("name=BrewClubContext")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Article>().ToTable("Articles");
			modelBuilder.Entity<Classified>().ToTable("Classifieds");
			modelBuilder.Entity<Recipe>().ToTable("Recipes");

			modelBuilder.Entity<IdentityUserLogin>().HasKey(u => u.UserId);
			modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
			modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
		}
		

		public DbSet<PostedItem> PostedItems { get; set; } 
		public DbSet<Article> Articles { get; set; }
		public DbSet<Classified> Classifieds { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
		

	}

}