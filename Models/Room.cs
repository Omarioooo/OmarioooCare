namespace OmarioooCare.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public Departments Department { get; set; }
        public RoomTypes RoomType { get; set; }
        public int NumOfPatientIntoRoom { get; set; }
        public int RoomCapacity { get; set; }
        public bool ISAvailable { get; set; }
    }
}
