using Mapster;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Models;

namespace OAuthServer.Service.Mapping;

internal class MapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // ENTITY TO DTO
        config.NewConfig<User, UserDto>();
    }
}