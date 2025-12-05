using FluentValidation;
using OAuthServer.Core.DTOs;

namespace OAuthServer.Service.Validations;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotNull().WithMessage("Refresh Token dolu olmalıdır")
            .NotEmpty().WithMessage("Refresh Token dolu olmalıdır");
    }
}