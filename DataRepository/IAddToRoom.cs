using OmarioooCare.Models;

namespace OmarioooCare.DataRepository
{
    public interface IAddToRoom : IAbleToAdd
    {
        public void AddToRoom(Patient patient);
    }
}
