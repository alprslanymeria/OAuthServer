using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.Services;
using OAuthServer.Core.UnitOfWork;
using System.Linq.Expressions;

namespace OAuthServer.Service.Services;

public class ServiceGeneric<TEntity, TDto>(

    IMapper mapper,
    IUnitOfWork unitOfWork,
    IGenericRepository<TEntity> repository) : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
{

    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<TEntity> _repository = repository;

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

        return Response<TDto>.Success(newDto, 201);
    }

    public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
    {
        // DATABASE'DEKİ TÜM ENTITY KAYITLARINI ÇEKER VE LIST DTO'YA DÖNÜŞTÜRÜR
        var dtos = _mapper.Map<List<TDto>>(await _repository.GetAllAsync().ToListAsync());

        return Response<IEnumerable<TDto>>.Success(dtos, 200);
    }

    public async Task<Response<TDto>> GetEntityByIdAsync(int id)
    {
        // DATABASE'DEKİ KAYITI ÇEKER.
        var entity = await _repository.GetEntityByIdAsync(id);

        // NULL CHECK / IF NULL RETURN 404
        if (entity == null)
        {
            return Response<TDto>.Fail("ID NOT FOUND!", true, 404);
        }

        // IF NOT NULL MAP ENTITY TO DTO
        var dto = _mapper.Map<TDto>(entity);

        return Response<TDto>.Success(dto, 200);
    }

    public async Task<Response<NoDataDto>> Remove(int id)
    {
        // CHECK ENTITY
        var isExistEntity = await _repository.GetEntityByIdAsync(id);

        if (isExistEntity == null)
        {
            return Response<NoDataDto>.Fail("ID NOT FOUND!", true, 404);
        }

        _repository.Remove(isExistEntity);

        await _unitOfWork.CommitAsync();

        return Response<NoDataDto>.Success(204);
    }

    public async Task<Response<NoDataDto>> Update(TDto dto, int id)
    {
        // CHECK ENTITY
        var isExistEntity = await _repository.GetEntityByIdAsync(id);

        if (isExistEntity == null)
        {
            return Response<NoDataDto>.Fail("ID NOT FOUND!", true, 404);
        }

        var entity = _mapper.Map<TEntity>(isExistEntity);

        _repository.Update(entity);

        await _unitOfWork.CommitAsync();

        return Response<NoDataDto>.Success(204);
    }

    public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await _repository.Where(predicate).ToListAsync();

        var dtos = _mapper.Map<IEnumerable<TDto>>(list);

        return Response<IEnumerable<TDto>>.Success(dtos, 200);
    }
}
