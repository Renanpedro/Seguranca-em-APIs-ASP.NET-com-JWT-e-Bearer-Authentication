using JwtAspNet.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();


var app = builder.Build();

app.MapGet("/", (TokenService service, ClaimsPrincipal User)
    => service.Create());

app.Run();
