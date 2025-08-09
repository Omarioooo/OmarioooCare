using Microsoft.Data.SqlClient;
using System.Data;
using OmarioooCare.DataAccess;
using OmarioooCare.Models;

namespace OmarioooCare.DataRepository
{
    public class DepartmentRepository : IDataRepository<Department>
    {
        private readonly IDbConnectionFactory? _factory;

        public DepartmentRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }
        public List<Department> GetAll()
        {
            List<Department> departmentsList = new List<Department>();

            // Prepare Connection
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Query
            using var cmd = new SqlCommand("select_Dep_info", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Open Conn
            sqlConn.Open();

            // Exec Query
            using var reader = cmd.ExecuteReader();

            // Bind data
            while (reader.Read())
            {
                Enum.TryParse(reader["depName"].ToString(), out Departments dep);
                Department department = new Department()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("depID")),
                    Dep = dep,
                    MangerID = reader.GetInt32(reader.GetOrdinal("MangerID")),
                    MangerName = reader["MangerID"].ToString(),
                    NumOfPatient = reader.GetInt32(reader.GetOrdinal("num_Of_Patients"))
                };

                departmentsList.Add(department);
            }

            return departmentsList;
        }
    }
}
