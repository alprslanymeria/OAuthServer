using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.Services;
using OAuthServer.Core.UnitOfWork;
using System.Linq.Expressions;
using System.Net;

namespace OAuthServer.Service.Services;

public class ServiceGeneric<TEntity, TDto>(

    IMapper mapper,
    IUnitOfWork unitOfWork,
    IGenericRepository<TEntity> repository) : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
{

    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<TEntity> _repository = repository;


    public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
    {
        var dtos = _mapper.Map<List<TDto>>(await _repository.GetAll().ToListAsync());

        return Response<IEnumerable<TDto>>.Success(dtos);
    }

    public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await _repository.Where(predicate).ToListAsync();

        var dtos = _mapper.Map<IEnumerable<TDto>>(list);

        return Response<IEnumerable<TDto>>.Success(dtos);
    }

    public async ValueTask<Response<TDto>> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
        {
            return Response<TDto>.Fail("ID NOT FOUND!", HttpStatusCode.NotFound);
        }

        var dto = _mapper.Map<TDto>(entity);

        return Response<TDto>.Success(dto);
    }

    public async Task<Response<TDto>> AddAsync(TDto dto)
    {
        // GELEN DTO NESNESİ ENTITY'E DÖNÜŞTÜRÜLÜR
        var entity = _mapper.Map<TEntity>(dto);

        // BU "entity" VARIABLE TÜM ALANLARI DOLDURULMUŞ ŞEKİLDE MEMORY'DE YENİ HALİ İLE DURUR.
        await _repository.AddAsync(entity);

        // DEĞİŞİKLİKLER VERİTABANINA KAYDEDİLİR
        await _unitOfWork.CommitAsync();

        // YENİ DEĞERLERİ İLE EKLENEN "entity" VARIABLE'INI TEKRAR DTO'YA DÖNÜŞTÜRÜYORUZ
        var newDto = _mapper.Map<TDto>(entity);

        return Response<TDto>.Success(newDto, HttpStatusCode.Created);
    }

    public async Task<Response> Update(TDto dto, int id)
    {
        // CHECK ENTITY
        var isExistEntity = await _repository.GetByIdAsync(id);

        if (isExistEntity is null)
        {
            return Response.Fail("ID NOT FOUND!", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<TEntity>(isExistEntity);

        _repository.Update(entity);

        await _unitOfWork.CommitAsync();

        return Response.Success(HttpStatusCode.NoContent);
    }

    public async Task<Response> Delete(int id)
    {
        // CHECK ENTITY
        var isExistEntity = await _repository.GetByIdAsync(id);

        if (isExistEntity is null)
        {
            return Response.Fail("ID NOT FOUND!", HttpStatusCode.NotFound);
        }

        _repository.Delete(isExistEntity);

        await _unitOfWork.CommitAsync();

        return Response.Success(HttpStatusCode.NoContent);
    }
    
}
