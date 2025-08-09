using Microsoft.Data.SqlClient;
using OmarioooCare.DataAccess;
using OmarioooCare.Models;
using System.Data;

namespace OmarioooCare.DataRepository
{
    public class RoomRepository : IDataRepository<Room>, IGetAllData<Room>, IAddToRoom
    {
        private readonly IDbConnectionFactory? _factory;

        public RoomRepository(IDbConnectionFactory? factory)
        {
            _factory = factory;
        }

        public List<Room> GetAll()
        {
            var rooms = new List<Room>();

            // Prepare Connection
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            // Prepare Query
            using var cmd = new SqlCommand("room_info", sqlConn);
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            // Open Connection
            sqlConn?.Open();

            // Exec Query
            using var reader = cmd.ExecuteReader();

            // Bind the data
            while (reader.Read())
            {
                Enum.TryParse(reader["DepName"].ToString(), out Departments department);
                Enum.TryParse(reader["Type"].ToString(), out RoomTypes roomType);
                var isAvailable = reader["Availability"].ToString().Equals("AVAILABLE", StringComparison.OrdinalIgnoreCase) ? true : false;
                var room = new Room()
                {
                    RoomNumber = reader.GetInt32(reader.GetOrdinal("RoomID")),
                    Department = department,
                    RoomType = roomType,
                    NumOfPatientIntoRoom = reader.GetInt32(reader.GetOrdinal("PatientCount")),
                    RoomCapacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                    ISAvailable = isAvailable
                };

                rooms.Add(room);
            }

            return rooms;
        }

        public void AddToRoom(Patient patient)
        {
            using var sqlConn = _factory?.CreateConnection();

            if (sqlConn == null)
                throw new InvalidOperationException("Database connection could not be created.");

            using var cmd = new SqlCommand("insertPatientIntoRoom", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };


            int.TryParse(patient.Phone, out var phoneNumber);

            int.TryParse(patient.Place, out var roomID);

            // Set Parameters
            cmd.Parameters.AddWithValue("@patientID", patient.Id);
            cmd.Parameters.AddWithValue("@FirstName", patient.FirstName ?? string.Empty);
            cmd.Parameters.AddWithValue("@SecondName", patient.SecondName ?? string.Empty);
            cmd.Parameters.AddWithValue("@ThirdName", patient.ThirdName ?? string.Empty);
            cmd.Parameters.AddWithValue("@Gender", patient.Gender.ToString()[0]);
            cmd.Parameters.AddWithValue("@Phone", phoneNumber);
            cmd.Parameters.AddWithValue("@City", patient.City.ToString());
            cmd.Parameters.AddWithValue("@RoomID", roomID);
            cmd.Parameters.AddWithValue("@StayingTime", patient.DaysToStayInRoom);

            // Open Connection
            sqlConn.Open();

            // Execute Query
            cmd.ExecuteNonQuery();
        }
    }
}