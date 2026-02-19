using System.Diagnostics;

namespace OAuthServer.Infrastructure.OpenTelemetry;

public static class ActivitySourceProvider
{
    public static ActivitySource Source = null!;
}
