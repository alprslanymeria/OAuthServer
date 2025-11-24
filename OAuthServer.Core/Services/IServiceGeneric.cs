using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OAuthServer.Core.Services
{
    // BURDA BULUNAN METOTLARIN IMPLAMENTASYONU SERVICE KATMANINDA YAPILIR.
    // BURADA BULUNAN METOTLAR SERVICE VEYA PRESENTATION (API) KATMANINDA KULLANILABİLİR.
    // METOTLARDAN DÖNEN VERİ İSE SERVICE VEYA PRESENTATION (API) KATMANINDA KULLANILABİLİR.

    public interface IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetEntityByIdAsync(int id);

        // BURADA DÖNEN VERİ TİPİ OLARAK GENERIC REPOSITORY'DEN FARKLI OLARAK IENUMERABLE KULLANDIK.
        // API KATMANINDA BUSINESS KOD ÇALIŞTIRMAYACAĞIZ.
        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        Task<Response<TDto>> AddAsync(TDto dto);

        Task<Response<NoDataDto>> Remove(int id);

        Task<Response<NoDataDto>> Update(TDto dto, int id);
    }
}
