using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UAZ_SI_2025.Business.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> CommitAsync();
        public IQueryable<T> Query();
    }
}
