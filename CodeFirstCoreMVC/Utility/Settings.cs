using DataAccess.Context;
using DataAccess.Factories;

namespace CodeFirstCoreMVC.Utility
{
    public static class Settings
    {
        public static SqlServerDbContext makeConnection()
        {
			try
			{
                var sqlFactory = new SqlServerDbContextFactory();
                var sqlConnection = sqlFactory.CreateDbContext(Array.Empty<string>());

                return sqlConnection;
            }

			catch (Exception)
			{
                return null;
			}
        }
    }
}
