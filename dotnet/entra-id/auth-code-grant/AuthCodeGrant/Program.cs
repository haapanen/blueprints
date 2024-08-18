using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

var app = builder.Build();

app.UseAuthentication();

app.MapGet("/", (HttpContext ctx) =>
{
    if (ctx.User.Identity?.IsAuthenticated ?? false)
    {
        return ctx.Response.WriteAsync($"Hello {ctx.User.Identity.Name}");
    }
    else
    {
        return ctx.ChallengeAsync();
    }
});

app.Run();
