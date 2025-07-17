using BlazorApp1.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using BlazorWebApp.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<BlazorWebAppContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazorWebAppContext") ?? throw new InvalidOperationException("Connection string 'BlazorWebAppContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<BlazorWebAppContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BlazorWebAppContext") ??
        throw new InvalidOperationException("Connection string 'BlazorWebAppContext' not found.")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorizationCore();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
})
.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOktaMvc(new OktaMvcOptions
{
    OktaDomain = builder.Configuration.GetValue<string>("Okta:OktaDomain"),
    AuthorizationServerId = builder.Configuration.GetValue<string>("Okta:AuthorizationServerId"),
    ClientId = builder.Configuration.GetValue<string>("Okta:ClientId"),
    ClientSecret = builder.Configuration.GetValue<string>("Okta:ClientSecret"),
    Scope = new List<string> { "openid", "profile", "email" },
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}



app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
