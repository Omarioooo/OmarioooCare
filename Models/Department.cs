namespace OmarioooCare.Models
{
    public class Department
    {
        public int Id { get; set; }

        public Departments Dep { get; set; }

        public int MangerID { get; set; }

        public string? MangerName { get; set; }

        public int NumOfPatient { get; set; }

    }
}
