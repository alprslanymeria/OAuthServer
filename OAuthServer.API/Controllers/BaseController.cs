using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.Helper;
using System.Net;

namespace OAuthServer.API.Controllers;

public class BaseController : ControllerBase
{
    [NonAction]
    public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
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

// REST Mimarisinde her response'ın bir status code değeri olmak zorundayken body'si olmak zorunda değildir.
// Bu bir yardımcı metot olduğu için "NonAction" attrbute kullandık.