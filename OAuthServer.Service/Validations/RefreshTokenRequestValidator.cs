using FluentValidation;
using OAuthServer.Core.DTOs.RefreshToken;

namespace OAuthServer.Service.Validations;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotNull().WithMessage("REFRESH TOKEN REQUIRED")
            .NotEmpty().WithMessage("REFRESH TOKEN REQUIRED");
    }
}