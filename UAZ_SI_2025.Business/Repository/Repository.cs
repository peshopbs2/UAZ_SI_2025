using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Data;

namespace UAZ_SI_2025.Business.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _set;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set.Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _set;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public IQueryable<T> Query()
        {
            return _set.AsQueryable();
        }

        public void Remove(T entity)
        {
            _set.Remove(entity);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }
    }
}
