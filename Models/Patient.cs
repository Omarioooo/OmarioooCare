namespace OmarioooCare.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? Phone { get; set; }
        public int DaysToStayInRoom { get; set; }
        public string? Place { get; set; }
        public string? State { get; set; }
        public DateOnly StartingDate { get; set; }
        public Gender Gender { get; set; }
        public Cities City { get; set; }
        public Departments Department { get; set; }
    }
}
