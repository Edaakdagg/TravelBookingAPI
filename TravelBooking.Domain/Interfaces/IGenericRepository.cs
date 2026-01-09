using TravelBooking.Domain; // BaseEntity (same namespace or global check)
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TravelBooking.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}