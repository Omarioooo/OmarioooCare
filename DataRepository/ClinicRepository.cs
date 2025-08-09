using Microsoft.Data.SqlClient;
using OmarioooCare.DataAccess;
using OmarioooCare.Models;
using System.Data;

namespace OmarioooCare.DataRepository
{
    public class ClinicRepository : IDataRepository<Clinic>, IBookAppointement
    {
        private readonly IDbConnectionFactory? _factory;

        public ClinicRepository(IDbConnectionFactory? factory)
        {
            _factory = factory;
        }

        public List<Clinic> GetAll()
        {
            List<Clinic> clinicsList = new List<Clinic>();

            // Perpare Conn
            using var sqlConn = _factory.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Query
            using var cmd = new SqlCommand("clinic_info", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Open Connection
            sqlConn.Open();

            // Exec Query
            using var reader = cmd.ExecuteReader();

            // bind data
            while (reader.Read())
            {
                Enum.TryParse(reader["Clinic"].ToString(), out Clinics clinicName);
                Enum.TryParse(reader["Department"].ToString(), out Departments department);
                var clinic = new Clinic()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ClinicID")),
                    ClinicName = clinicName,
                    Department = department,
                    DoctorName = reader["Doctor"].ToString()
                };

                clinicsList.Add(clinic);
            }

            return clinicsList;
        }

        public void AddAppointemnt(Patient patient, int appointmentID, decimal cost, string status)
        {
            // Prepare Connection
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Query
            using var cmd = new SqlCommand("bookAppointment", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };


            // Set Parameters

            int.TryParse(patient.Phone, out var phoneNumber);

            // Set Parameters
            cmd.Parameters.AddWithValue("@patientID", patient.Id);
            cmd.Parameters.AddWithValue("@FirstName", patient.FirstName ?? string.Empty);
            cmd.Parameters.AddWithValue("@SecondName", patient.SecondName ?? string.Empty);
            cmd.Parameters.AddWithValue("@ThirdName", patient.ThirdName ?? string.Empty);
            cmd.Parameters.AddWithValue("@Phone", phoneNumber);
            cmd.Parameters.AddWithValue("@City", patient.City.ToString());
            cmd.Parameters.AddWithValue("@Gender", patient.Gender.ToString()[0]);
            cmd.Parameters.AddWithValue("@Clinic_name", patient.Place.ToString());
            cmd.Parameters.AddWithValue("@Appointment_ID", appointmentID);
            cmd.Parameters.AddWithValue("@cost", cost);
            cmd.Parameters.AddWithValue("@status", status);

            // Open Connection
            sqlConn.Open();

            // Exec Query
            cmd.ExecuteNonQuery();
        }
    }
}
