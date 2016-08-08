namespace mbsoft.BrewClub.Data
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using Microsoft.AspNet.Identity.EntityFramework;

	public class BrewClubDbContext : IdentityDbContext<User>
	{
		public BrewClubDbContext() : base("name=BrewClubContext")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
	        base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostedItem>().HasRequired(x => x.Author);

            //modelBuilder.Entity<IdentityUserLogin>().HasKey(u => u.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
		
        public virtual DbSet<PostedItem> PostedItems { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Classified> Classifieds { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<ArticleComment> ArticleComments { get; set; }
		
    }

}