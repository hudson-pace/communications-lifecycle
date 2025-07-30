using BlazorApp1.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using BlazorWebApp.Data;
using BlazorApp1.Services;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<BlazorWebAppContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BlazorWebAppContext") ??
        throw new InvalidOperationException("Connection string 'BlazorWebAppContext' not found.")));

builder.Services.AddScoped<MovieApiService>();
builder.Services.AddHttpClient("MoviesApi", client =>
{
    client.BaseAddress = new Uri("http://movie-api:8080");
});

builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IForumPostService, ForumPostService>();
builder.Services.AddScoped<IForumCommentService, ForumCommentService>();

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlazorWebAppContext>();
    dbContext.Database.Migrate();
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
