using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.Education;
using Resume.Domain.Entities.User;

namespace Resume.Domain.Context
{
    public class DatabaseContext : DbContext
    {

	    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

	   

	    #region On Model Creating

	    protected override void OnModelCreating(ModelBuilder modelBuilder)
	    {
		    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
		    {
				relationship.DeleteBehavior = DeleteBehavior.Cascade;
		    }

		    base.OnModelCreating(modelBuilder);
	    }

		#endregion


		#region User

		public DbSet<User> Users { get; set; }

		#endregion

		#region Resume

        #region Education

        public DbSet<Education> Educations { get; set; }

        #endregion

		#endregion
	}
}
