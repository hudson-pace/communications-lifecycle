using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommLifecycle.Web.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using CommLifecycle.Web.Services;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<MovieApiService>();

var factory = new ConnectionFactory
{
    HostName = "host.docker.internal",
    UserName = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? string.Empty,
    Password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? string.Empty,
};
var connection = await factory.CreateConnectionAsync();
builder.Services.AddSingleton(connection);

// AddHostedService registers as IHostedService. Add as singleton first to register as RabbitPublisher.
builder.Services.AddSingleton<RabbitPublisher>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<RabbitPublisher>());

builder.Services.AddHostedService<RabbitConsumer>();

builder.Services.AddHttpClient("CommLifecycle.Api", client =>
{
    client.BaseAddress = new Uri("http://api:8080");
});

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

builder.Services.Configure<OpenIdConnectOptions>(OktaDefaults.MvcAuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "roles"
    };
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

app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
