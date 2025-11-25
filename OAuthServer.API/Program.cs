using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.Models;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.Services;
using OAuthServer.Core.UnitOfWork;
using OAuthServer.Data;
using OAuthServer.Data.Repositories;
using OAuthServer.Data.UnitOfWork;
using OAuthServer.Service.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

// DI REGISTER
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));

// DB CONTEXT
builder.Services.AddDbContext<AppDbContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {

        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
    });
});

// IDENTITY API
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// BİR TOKEN GELDİĞİ ZAMAN DOĞRULAMAK İÇİN BU KODLARI YAZIYORUZ.
// SCHEME --> UYGULAMADA BULUNAN FARKLI ÜYELİK SİSTEMLERİNİ BELİRTİR. MESELA BİRİ BAYİLER İÇİN DİĞERİ KULLANICILAR İÇİN
// ADD AUTHENTICATION İLE BU UYGULAMANIN BİR KİMLİK DOĞRULAMAYA SAHİP OLACAĞINI BELİRTİYORUZ.
// ADD JWT BEARER İLE REQUEST'TE GELEN TOKEN'İN DOĞRULANMA MEKANİZMASI EKLENMİŞ OLUR.

// ADD AUTHENTICATION --> KRALLIĞIN BAŞ SORUMLUSU
// SCHEMA'LAR --> BAŞ SORUMLUNUN ALTINDA ÇALIŞAN KAPI MUHAFIZLARI. BEARER BU SAVAŞÇILARDAN BİR TANESİYDİ...
// ADD JWT BEARER İLE BİRLİKTE BU SAVAŞÇI ÇEŞİTLİ SİLAHLAR İLE DONATILIR.

//AddAuthentication: “Krallığın kimlik doğrulama sistemi açılsın, varsayılan savaşçı şudur.”
//AddJwtBearer: “Bearer savaşçısına JWT doğrulama silahlarını ver.”
//DefaultAuthenticateScheme: “Kapıda genelde kim dursun?”
//DefaultChallengeScheme: “Kimlik gerekirse kimi çağırayım?”
//Birden fazla scheme: “Birden fazla kapı muhafızı” anlamına gelir.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOption>();

    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymetricKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ClockSkew = TimeSpan.Zero
    };
}).AddOAuth("Google", config =>
{
    config.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    config.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    config.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    config.CallbackPath = "/signin-google";
    config.SaveTokens = true;
    config.TokenEndpoint = "https://oauth2.googleapis.com/token";


});

// MAPSTER
builder.Services.AddMapster();
// OPTIONS PATTERN
builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOptions"));
//builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
