namespace OmarioooCare.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public Clinics ClinicName { get; set; }
        public Departments Department { get; set; }
        public string? DoctorName { get; set; }
    }
}
