using OmarioooCare.Models;

namespace OmarioooCare.DataRepository
{
    public interface IBookAppointement : IAbleToAdd
    {
        public void AddAppointemnt(Patient patient, int appointmentID, decimal cost, string status);
    }
}
