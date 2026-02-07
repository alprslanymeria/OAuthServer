using OAuthServer.Core.Helper;
using System.Linq.Expressions;

namespace OAuthServer.Core.Services;

// THE METHODS IN THIS INTERFACE ARE IMPLEMENTED IN THE SERVICE LAYER.
// THE METHODS IN THIS INTERFACE CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.
// THE DATA RETURNED FROM THE METHODS CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.

public interface IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
{
    Task<Response<IEnumerable<TDto>>> GetAllAsync();

    Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

    ValueTask<Response<TDto>> GetByIdAsync(int id);

    Task<Response<TDto>> AddAsync(TDto dto);

    Task<Response> Update(TDto dto, int id);

    Task<Response> Delete(int id);
}