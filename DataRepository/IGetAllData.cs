namespace OmarioooCare.DataRepository
{
    public interface IGetAllData<T>
    {
        public List<T> GetAll();
    }
}
