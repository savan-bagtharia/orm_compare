using ORMCompare.EFModel;
using System.Data.Entity;

namespace ORMCompare.DataLayer
{
    class EfDbContext : DbContext
    {
        public EfDbContext(string connectionString): base()
        {
            this.Database.Connection.ConnectionString = connectionString;
        }

        public virtual DbSet<TeamMemberEF> TeamMembers { get; set; }
        public virtual DbSet<ProjectEF> Projects { get; set; }
        public virtual DbSet<TeamEF> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEF>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TeamEF>()
                .HasMany(e => e.TeamMembers)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
