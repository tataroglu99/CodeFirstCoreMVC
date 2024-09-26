using DataAccess.Configurations;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Unit> Unit { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UnitsConfiguration());
        }
    }
}
