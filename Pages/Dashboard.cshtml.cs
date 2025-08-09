using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OmarioooCare.DataRepository;
using OmarioooCare.Models;

namespace OmarioooCare.Pages
{
    public class DashboardModel : PageModel
    {

        public Hospital Hospital { get; private set; }
        public List<Patient> PatientsList { get; private set; }
        public List<Clinic> ClinicsList { get; private set; }
        public List<MedicalStaff> MedicalStaffList { get; private set; }
        public List<Department> DepartmentsList { get; private set; }
        public List<Room> RoomsList { get; private set; }

        [BindProperty(SupportsGet = true)]
        public Patient patient { get; set; } = new Patient();

        [BindProperty]
        public int AppointmentID { get; set; }

        [BindProperty]
        public decimal Cost { get; set; }

        [BindProperty]
        public string Status { get; set; }

        private DateTime now = DateTime.Now;
        public DateTime Now
        {
            get
            {
                return now;
            }
        }
        private readonly IDataRepository<Hospital> _hospitalRepository;
        private readonly IDataRepository<Patient> _patientRepository;
        private readonly IDataRepository<Clinic> _clinicRepository;
        private readonly IDataRepository<MedicalStaff> _medicalStaffRepository;
        private readonly IDataRepository<Department> _departmentRepository;
        private readonly IDataRepository<Room> _roomRepositroy;

        public DashboardModel(IDataRepository<Hospital> hospitalRepository,
            IDataRepository<Patient> patientRepository,
            IDataRepository<Clinic> clinicRepository,
            IDataRepository<MedicalStaff> medicalStaffRepository,
            IDataRepository<Department> departmentRepository,
            IDataRepository<Room> roomRepository)
        {
            _hospitalRepository = hospitalRepository;
            _patientRepository = patientRepository;
            _clinicRepository = clinicRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _departmentRepository = departmentRepository;
            _roomRepositroy = roomRepository;
        }

        public void OnGet()
        {

            var hospitalRepository = _hospitalRepository as HospitalRepository;
            Hospital = new Hospital()
            {
                NumberOfCurrentPatients = hospitalRepository.GetNumberOfPatients(),
                NumberOfAvailableBeds = hospitalRepository.GetNumberOfAvailableBeds(),
                NumberOfMedicalStaff = hospitalRepository.GetNumberOfMedicalStaff(),
                NumberOfAppointements = hospitalRepository.GetNumberOfAppointements()
            };


            var patientRepository = _patientRepository as PatientRepository;
            PatientsList = patientRepository?.GetAll();

            var clinicRepository = _clinicRepository as ClinicRepository;
            ClinicsList = clinicRepository?.GetAll();

            var medicalRepository = _medicalStaffRepository as MedicalStaffRepository;
            MedicalStaffList = medicalRepository?.GetAll();

            var departmentRepositroy = _departmentRepository as DepartmentRepository;
            DepartmentsList = departmentRepositroy?.GetAll();

            var roomRepository = _roomRepositroy as RoomRepository;
            RoomsList = roomRepository?.GetAll();
        }

        public IActionResult OnPostAddToRoom()
        {

            var roomRepository = _roomRepositroy as RoomRepository;

            roomRepository.AddToRoom(patient);

            return RedirectToPage("/Dashboard");

        }

        public IActionResult OnPostBookAppointment()
        {
            var clinicRepository = _clinicRepository as ClinicRepository;

            clinicRepository.AddAppointemnt(patient, AppointmentID, Cost, Status);

            return RedirectToPage("/Dashboard");
        }
    }
}