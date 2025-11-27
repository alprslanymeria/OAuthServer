using Mapster;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Models;

namespace OAuthServer.Service.Mapster;

internal class MapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // ENTITY TO DTO
        config.NewConfig<User, UserDto>();
    }
}