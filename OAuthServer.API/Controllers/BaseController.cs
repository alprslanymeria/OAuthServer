using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.Helper;
using System.Net;

namespace OAuthServer.API.Controllers;

public class BaseController : ControllerBase
{
    [NonAction]
    public IActionResult ActionResultInstance<T>(Response<T> response)
    {
        return response.Status switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.Created => Created(response.UrlAsCreated, response),
            _ => new ObjectResult(response) { StatusCode = response.Status.GetHashCode() }
        };
    }

    [NonAction]
    public IActionResult ActionResultInstance(Response response)
    {
        return response.Status switch
        {
            HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = response.Status.GetHashCode() },
            _ => new ObjectResult(response) { StatusCode = response.Status.GetHashCode() }
        };
    }
}

// IN REST ARCHITECTURE, EVERY RESPONSE MUST HAVE A STATUS CODE BUT NOT NECESSARILY A BODY.
// WE USE "NON ACTION" ATTRIBUTE BECAUSE THIS IS A HELPER METHOD.