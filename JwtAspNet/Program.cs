using JwtAspNet;
using JwtAspNet.Extensions;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", p => p.RequireRole("admin"));
});


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/Login", (TokenService service)
    =>
{
    var user = new User(
        1,
        "Andre Baltireri",
        "XYZ@blta.io",
        "http:\\balta.io",
        "XYz",
        new[] { "student", "premium" });

    return service.Create(user);
});


app.MapGet("/restrito", (ClaimsPrincipal user) => new
    { 
       id = user.Id(),
       name = user.Name(),
       email = user.Email(),
       givenName = user.GivenName(),
       image = user.Image(),
    })
    .RequireAuthorization(); //RequireAuthorization sempre ir� buscar pelas polices

app.MapGet("/admin", () => "Voc� tem acesso!")
    .RequireAuthorization("Admin");



app.Run();
