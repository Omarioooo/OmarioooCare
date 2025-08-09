using Microsoft.Data.SqlClient;

namespace OmarioooCare.DataAccess
{

    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}