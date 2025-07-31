using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;
using SharedModels.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MoviesApiContext>(opt =>
    opt.UseInMemoryDatabase("Movies"));
builder.Services.AddScoped<ICommunicationService, CommunicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/movies", async (MoviesApiContext context) =>
{
    Console.WriteLine("Getting movies...");
    return await context.Movies.ToListAsync();
});
app.MapPost("/movies", async (Movie movie, MoviesApiContext context) =>
{
    context.Movies.Add(movie);
    await context.SaveChangesAsync();
    return Results.Created($" / movies /{movie.Id}", movie);
});

app.MapGet("/communications", async (ICommunicationService communicationService) =>
{
    return await communicationService.GetAllCommunicationsAsync();
});
app.MapGet("/communications/{id}", async (ICommunicationService communicationService, int id) =>
{
    return await communicationService.GetCommunicationAsync(id);
});

app.MapGet("/CommunicationTypes", async (ICommunicationService communicationService) =>
{
    return await communicationService.GetAllCommunicationTypesAsync();
});
app.MapGet("/CommunicationTypes/{id}", async (ICommunicationService communicationService, int id) =>
{
    return await communicationService.GetCommunicationTypeAsync(id);
});

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesApiContext>();
SeedDb s = new();
await s.Seed(context);

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}