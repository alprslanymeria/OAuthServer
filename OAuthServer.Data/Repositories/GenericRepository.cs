using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OAuthServer.Data.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();


        // BU METOTLARDA YAPILAN İŞLEMLER VERİTABANINA YANSIMAZ, NE ZAMAN Kİ SERVIC KATMANINDA 
        // SaveChangesAsync() METODU ÇAĞRILIRSA O ZAMAN YANSIR.
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return entity;
            }

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            //| Yöntem             | Kısa Açıklama                           | Tavsiye             |
            //| ------------------ | --------------------------------------- | ------------------  |
            //| `Update()`         | Çok agresif, tüm graph'ı günceller      | ❌ Kaçın            |
            //| `State = Modified` | Kontrollü ama tüm kolonları update eder | ⚠️ Bazı durumlarda  |
            //| `SetValues()`      | En güvenli, en temiz update yöntemi     | ✅ Şiddetle tavsiye |

            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);

            // NE ZAMAN Kİ SERVİCE KATMANINDA ToListAsync() ÇAĞRILIRSA O ZAMAN VERİTABANIA YANSIR.
        }
    }
}
