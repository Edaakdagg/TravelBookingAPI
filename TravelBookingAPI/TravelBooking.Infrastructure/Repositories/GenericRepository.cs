using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain; // BaseEntity'yi bulmak için
using TravelBooking.Infrastructure.Interfaces;
using TravelBooking.Infrastructure.Data; // KRİTİK DÜZELTME: Doğru DbContext namespace'i
using System.Linq.Expressions;
using System.Collections.Generic; // IEnumerable için

namespace TravelBooking.Infrastructure.Repositories
{
    // T'nin BaseEntity olduğu garantilidir
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        // KRİTİK DÜZELTME: ApplicationDbContext yerine AppDbContext kullanıldı
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // BaseEntity'de IsDeleted olduğu için artık sadece silinmemiş kayıtları getirebiliriz.
        private IQueryable<T> ApplySoftDeleteFilter()
        {
            // Bu sadece BaseEntity'den miras alanlar için çalışacaktır.
            return _context.Set<T>().Where(e => !e.IsDeleted); 
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            // FindAsync metodu, birincil anahtar (primary key) üzerinde çalıştığı için 
            // global filtre (AppDbContext'e eklenen) yumuşak silme kontrolünü otomatik yapar.
            return await _context.Set<T>().FindAsync(id); 
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Soft Delete filtresi uygulanıyor
            return await ApplySoftDeleteFilter().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            // Soft Delete filtresi ve ek koşul birleştiriliyor
            return await ApplySoftDeleteFilter().Where(expression).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow; // Güncelleme zamanı ekleniyor
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            // Fiziksel silme yerine yumuşak silme uygulanır
            entity.IsDeleted = true; 
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Bu metot IUserService/Diğer Service'ler için genel bir koşullu getirme sağlar.
        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            // Soft delete filtresi otomatik uygulanır.
            return await ApplySoftDeleteFilter().Where(expression).ToListAsync();
        }
    }
}