using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace OAuthServer.Service.Filters;

public class FluentValidationFilter : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        var errors = context.ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                            .ToList();

        var responseModel = Response.Fail(errors);

        return new BadRequestObjectResult(responseModel);
    }
}
