using Microsoft.EntityFrameworkCore;
using ORMCompare.EFModel;

namespace ORMCompare.DataLayer
{
    class EfCoreDbContext : DbContext
    {
        private readonly string _connectionString;

        public EfCoreDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<TeamMemberEF> TeamMembers { get; set; }
        public virtual DbSet<ProjectEF> Projects { get; set; }
        public virtual DbSet<TeamEF> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEF>().HasMany(e => e.Teams);

            modelBuilder.Entity<TeamEF>().HasMany(e => e.TeamMembers);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
