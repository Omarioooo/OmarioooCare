using Microsoft.Data.SqlClient;
using OmarioooCare.DataAccess;
using OmarioooCare.Models;
using System.Data;

namespace OmarioooCare.DataRepository
{
    public class HospitalRepository : IDataRepository<Hospital>
    {
        private readonly IDbConnectionFactory? _factory;

        public HospitalRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public int GetNumberOfPatients()
        {
            // Perpare Conn
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                if (sqlConn == null)
                    throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Qurey
            using var cmd = new SqlCommand("NumOfPatients", sqlConn) { CommandType = CommandType.StoredProcedure };

            // Open Conntion
            sqlConn.Open();

            // Exec Query
            var result = cmd.ExecuteScalar();

            int.TryParse(result.ToString(), out int total);

            return total;
        }

        public int GetNumberOfAvailableBeds()
        {
            // Perpare Conn
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                if (sqlConn == null)
                    throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Qurey
            using var cmd = new SqlCommand("totalNumberOfAvailableBeds", sqlConn) { CommandType = CommandType.StoredProcedure };

            // Open Conntion
            sqlConn.Open();

            // Exec Query
            var result = cmd.ExecuteScalar();

            int.TryParse(result.ToString(), out int total);

            return total;
        }

        public int GetNumberOfMedicalStaff()
        {
            // Perpare Conn
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                if (sqlConn == null)
                    throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Qurey
            using var cmd = new SqlCommand("NumOfNurses", sqlConn) { CommandType = CommandType.StoredProcedure };

            // Open Conntion
            sqlConn.Open();

            // Exec Query
            var result = cmd.ExecuteScalar();

            int.TryParse(result.ToString(), out int total);

            return total;
        }

        public int GetNumberOfAppointements()
        {
            // Perpare Conn
            var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                if (sqlConn == null)
                    throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Qurey
            var cmd = new SqlCommand("total_appointments", sqlConn) { CommandType = CommandType.StoredProcedure };

            // Open Conntion
            sqlConn.Open();

            // Exec Query
            var result = cmd.ExecuteScalar();

            int.TryParse(result.ToString(), out int total);

            return total;
        }
    }
}
