using Microsoft.AspNetCore.Mvc;
using OAuthServer.API.ExceptionHandlers;
using OAuthServer.API.Extensions;
using OAuthServer.API.Filters;
using OAuthServer.API.ModelBinding;
using OAuthServer.API.Middlewares;
using OAuthServer.Core.Configuration;
using OAuthServer.Data.Extensions;
using OAuthServer.Service.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// SERVICES
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new FileUploadModelBinderProvider());
});
builder.Services.AddOpenApi();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services
    .AddRepositories(builder.Configuration)
    .AddServices()
    .AddCustomTokenAuth(builder.Configuration)
    .AddOpenTelemetryServicesExt(builder.Configuration)
    .AddStorageServicesExt(builder.Configuration); ;


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

// MIDDLEWARES
app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<OpenTelemetryTraceIdMiddleware>();
app.UseMiddleware<RequestAndResponseActivityMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();