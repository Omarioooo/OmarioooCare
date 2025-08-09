using Microsoft.Data.SqlClient;
using System.Data;

namespace OmarioooCare.DataAccess
{
    public class LoginChecking : ILoginService
    {
        private readonly IDbConnectionFactory? _factory;

        public LoginChecking(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public bool LoginCheck(string username, int ID)
        {
            // Perpare Connection
            var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Perpate Query
            var cmd = new SqlCommand("login_check", sqlConn)
            {
                CommandType = CommandType.StoredProcedure,
            };

            // Set Parameters
            cmd.Parameters.AddWithValue("@Name", username);
            cmd.Parameters.AddWithValue("@ID", ID);

            // Output parameter
            var outputParam = new SqlParameter("@Message", SqlDbType.VarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            // Open connection
            sqlConn.Open();

            // Execute stored procedure
            cmd.ExecuteNonQuery();

            // Read output value
            var message = outputParam.Value?.ToString();

            return string.Equals(message, "Ok", StringComparison.OrdinalIgnoreCase);
        }
    
    }
}
