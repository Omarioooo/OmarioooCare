using Microsoft.Data.SqlClient;
using OmarioooCare.DataAccess;
using OmarioooCare.Models;
using System.Data;

namespace OmarioooCare.DataRepository
{
    public class PatientRepository : IDataRepository<Patient>, IGetAllData<Patient>
    {

        private readonly IDbConnectionFactory? _factory;

        public PatientRepository(IDbConnectionFactory? factory)
        {
            _factory = factory;
        }


        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();

            // Prepare the connection
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Prepare the Query
            using var cmd = new SqlCommand("get_all_patients", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Open the connection
            sqlConn?.Open();

            // Exec the Query
            using var reader = cmd.ExecuteReader();

            // bind the data
            while (reader.Read())
            {
                Enum.TryParse(reader["depName"].ToString(), out Departments departmentEnum);

                var patient = new Patient
                {
                    Id = reader.GetInt32(reader.GetOrdinal("PatientID")),
                    FirstName = reader["FirstName"]?.ToString(),
                    SecondName = reader["SecondName"]?.ToString(),
                    State = reader["State"]?.ToString()
                };

                patients.Add(patient);
            }
            return patients;
        }

    }
}
