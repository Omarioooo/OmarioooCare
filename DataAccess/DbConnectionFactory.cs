using Microsoft.Data.SqlClient;

namespace OmarioooCare.DataAccess
{
    public class DbConnectionFactory : IDbConnectionFactory
    {

        private readonly string? _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}