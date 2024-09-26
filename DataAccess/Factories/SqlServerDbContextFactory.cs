using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Factories
{
    public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {
        public SqlServerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
            var connectionString = "Data Source=DESKTOP-09L5NEP;Initial Catalog=dbCodeFirst;Integrated Security=True;TrustServerCertificate=True"; //Environment.GetEnvironmentVariable("SQLSERVER_MOVIES_LOCAL_CONNSTR");
            optionsBuilder.UseSqlServer(connectionString
                                        ?? throw new NullReferenceException(
                                            $"Connection string is not valid {nameof(connectionString)}"));

            return new SqlServerDbContext(optionsBuilder.Options);
        }
    }
}