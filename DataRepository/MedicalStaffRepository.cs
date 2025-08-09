using Microsoft.Data.SqlClient;
using OmarioooCare.DataAccess;
using OmarioooCare.Models;
using System.Data;

namespace OmarioooCare.DataRepository
{
    public class MedicalStaffRepository : IDataRepository<MedicalStaff>, IGetAllData<MedicalStaff>
    {
        private readonly IDbConnectionFactory? _factory;

        public MedicalStaffRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<MedicalStaff> GetAll()
        {
            List<MedicalStaff> staff = new List<MedicalStaff>();

            // Prepare Connection
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Query
            using var cmd = new SqlCommand("nurse_info", sqlConn) { CommandType = CommandType.StoredProcedure };

            // Open Connection
            sqlConn.Open();

            // Exec Query
            using var reader = cmd.ExecuteReader();

            // Bind data
            while (reader.Read())
            {
                Enum.TryParse(reader["Department"].ToString(), out Departments department);
                var medicalStaff = new MedicalStaff()
                {
                    Name = reader["Name"].ToString(),
                    RoomNumber = reader.GetInt32(reader.GetOrdinal("RoomID")),
                    Manger = reader["Manager"].ToString(),
                    Department = department
                };

                staff.Add(medicalStaff);
            }

            return staff;
        }
    }
}
