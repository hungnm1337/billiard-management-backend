using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.IBaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly Prn232ProjectContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(Prn232ProjectContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

        }

    }
}
