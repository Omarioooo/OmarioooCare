namespace OmarioooCare.Models
{
    public class MedicalStaff
    {
        public string? Name { get; set; }
        public int RoomNumber { get; set; }
        public string? Manger { get; set; }
        public Departments Department { get; set; }
    }
}
