using System.Net;

namespace OAuthServer.Core.Exceptions;

public class UnauthorizedException(string message) : AppException(message, HttpStatusCode.Unauthorized);
