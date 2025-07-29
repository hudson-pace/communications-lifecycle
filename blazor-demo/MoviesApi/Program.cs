using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

static async Task SeedDbAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MoviesApiContext>();

    CommunicationType EobType = new() { Name = "EOB" };
    CommunicationType EopType = new() { Name = "EOP" };
    CommunicationType IdCardType = new() { Name = "ID Card" };

    context.CommunicationTypes.Add(EobType);
    context.CommunicationTypes.Add(EopType);
    context.CommunicationTypes.Add(IdCardType);
    await context.SaveChangesAsync();

    List<CommunicationStatusChange> statusHistory = [];

    Communication Eob1 = new()
    {
        Title = "Eob 1",
        Type = EobType,
        StatusHistory = [
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EobType,
                    Description = "Released",
                }
            },
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EobType,
                    Description = "Printed",
                }
            }
        ],
    };
    Communication Eop1 = new()
    {
        Title = "Eop 1",
        Type = EopType,
        StatusHistory = [
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EopType,
                    Description = "Released",
                }
            },
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EopType,
                    Description = "Delivered",
                }
            }
        ],
    };
    Communication IdCard1 = new()
    {
        Title = "ID Card 1",
        Type = IdCardType,
        StatusHistory = [
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = IdCardType,
                    Description = "QueuedForPrinting",
                }
            },
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = IdCardType,
                    Description = "Printed",
                }
            }
        ],
    };

    context.Communications.Add(Eob1);
    context.Communications.Add(Eop1);
    context.Communications.Add(IdCard1);
    await context.SaveChangesAsync();

    Console.WriteLine("More Seed to Sow");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MoviesApiContext>(opt =>
    opt.UseInMemoryDatabase("Movies"));

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
    return Results.Created($" / movies /{ movie.Id}", movie);
});
await SeedDbAsync(app.Services);
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}