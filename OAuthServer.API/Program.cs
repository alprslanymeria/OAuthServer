using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OAuthServer.API.ExceptionHandlers;
using OAuthServer.API.Extensions;
using OAuthServer.API.Filters;
using OAuthServer.API.Middlewares;
using OAuthServer.API.ModelBinding;
using OAuthServer.Core.Configuration;
using OAuthServer.Data;
using OAuthServer.Infrastructure;
using OAuthServer.Infrastructure.OpenTelemetry;
using OAuthServer.Service;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// OPEN TELEMETRY
builder.AddOpenTelemetryLogExt();

// SERVICES
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new FileUploadModelBinderProvider());
});
builder.Services.AddOpenApi();

// HEALTH CHECKS
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")!,
        name: "sqlserver",
        tags: ["db", "ready"]);

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
        else
        {
            var allowedOrigins = builder.Configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>() ?? [];

            policy.WithOrigins(allowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
    });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services
    .AddRepositories(builder.Configuration)
    .AddServices()
    .AddInfrastructure(builder.Configuration)
    .AddCustomTokenAuth(builder.Configuration)
    .AddOpenTelemetryServicesExt(builder.Configuration);


// FLUENT VALIDATION AUTO VALIDATION
builder.Services.AddFluentValidationAutoValidation(cfg =>
{
    cfg.OverrideDefaultResultFactoryWith<FluentValidationFilter>();
});

// EXCEPTION HANDLERS
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// OPTIONS PATTERN
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));
builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOptions"));

var app = builder.Build();

app.UseExceptionHandler(x => { });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// HEALTH CHECK ENDPOINTS
app.MapHealthChecks("/health/live", new()
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new()
{
    Predicate = check => check.Tags.Contains("ready")
});



// MIDDLEWARES
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors();
app.UseMiddleware<OpenTelemetryTraceIdMiddleware>();
app.UseMiddleware<RequestAndResponseActivityMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();