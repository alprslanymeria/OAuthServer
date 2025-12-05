using System.Linq.Expressions;

namespace OAuthServer.Core.Repositories;

// BURDA BULUNAN METOTLARIN IMPLAMENTASYONU DATA KATMANINDA YAPILIR.
// BURADA BULUNAN METOTLAR DATA VEYA SERVICE KATMANINDA KULLANILABİLİR.
// METOTLARDAN DÖNEN VERİ İSE DATA VEYA SERVICE KATMANINDA KULLANILABİLİR.

public interface IGenericRepository<TEntity> where TEntity : class
{
    // ENTITY'LER İÇİN YAPILAN GENEL İŞLEMLER YANİ CRUD İŞLEMLERİ BURADA TANIMLANACAK.
    // KÜÇÜK VE ORTA BAZLI PROJELERDE GENERİC REPOSİTORY PATTERN YETERLİDİR. DAHA BÜYÜK SEVİYELERDE DDD KULLANILIR.

    // ToList() METODUNU ÇAĞIRANA KADAR IQueryable VERİTABANINA YANSIMAZ.
    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    ValueTask<TEntity?> GetByIdAsync(int id);

    ValueTask AddAsync(TEntity entity);

    TEntity Update(TEntity entity);

    void Delete(TEntity entity);
}