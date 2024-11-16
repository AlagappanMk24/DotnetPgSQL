using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetPgSQL.Infrastructure.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        public async Task<T> GetByIdAsync(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            return model;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var models = await _context.Set<T>().AsNoTracking().ToListAsync();
            return models;
        }
        public async Task<T> GetByNameAsync(string name)
        {
            var model = await _context.Set<T>().FindAsync(name);
            return model;
        }
        public async Task<T> AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            return model;
        }
        public async Task<T> UpdateAsync(T model)
        {
            T entity = await _context.Set<T>().FindAsync(model);
            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(model);
                _context.Entry(entity).State = EntityState.Modified;
                return entity;
            }
            return null;
        }
        public async Task<T> DeleteAsync(int Id)
        {
            T entity = await _context.Set<T>().FindAsync(Id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                //await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }
    }
}
