using System.Data.SqlClient;

namespace WalletService.Api.App_Start
{
    public class DbInitializer
    {
        public static void EnsureDatabaseExists(string databaseName)
        {
            var connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=password123;";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $@"
                    IF DB_ID(N'{databaseName}') IS NULL
                    BEGIN
                        CREATE DATABASE [{databaseName}];
                    END";
                cmd.ExecuteNonQuery();
            }
        }
    }
}