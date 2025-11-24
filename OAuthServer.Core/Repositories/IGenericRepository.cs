using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OAuthServer.Core.Repositories
{
    // BURDA BULUNAN METOTLARIN IMPLAMENTASYONU DATA KATMANINDA YAPILIR.
    // BURADA BULUNAN METOTLAR DATA VEYA SERVICE KATMANINDA KULLANILABİLİR.
    // METOTLARDAN DÖNEN VERİ İSE DATA VEYA SERVICE KATMANINDA KULLANILABİLİR.

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // ENTITY'LER İÇİN YAPILAN GENEL İŞLEMLER YANİ CRUD İŞLEMLERİ BURADA TANIMLANACAK.
        // KÜÇÜK VE ORTA BAZLI PROJELERDE GENERİC REPOSİTORY PATTERN YETERLİDİR. DAHA BÜYÜK SEVİYELERDE DDD KULLANILIR.

        Task<TEntity> GetEntityByIdAsync(int id);

        // ToList() METODUNU ÇAĞIRANA KADAR IQueryable VERİTABANINA YANSIMAZ.
        IQueryable<TEntity> GetAllAsync();

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        TEntity Update(TEntity entity);
    }
}
