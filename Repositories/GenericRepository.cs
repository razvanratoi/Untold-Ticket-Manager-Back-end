using Assignment1.Data;
using Assignment1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public  async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> Add(T t)
        {
            _context.Set<T>().Add(t);
            return await SaveChanges();
        }

        public async Task<bool> Update(T t)
        {
            _context.Set<T>().Update(t);
            return await SaveChanges();
        }

        public async Task<bool> Delete(T t)
        {
            _context.Set<T>().Remove(t);
            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}