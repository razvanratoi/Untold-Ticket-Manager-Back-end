namespace Assignment1.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T t);
        Task<bool> Update(T t);
        Task<bool> Delete(T t);
        Task<bool> SaveChanges(); 
    }
}