using System.Net;

namespace OAuthServer.Core.Exceptions;

public class NotFoundException(string message) : AppException(message, HttpStatusCode.NotFound);
