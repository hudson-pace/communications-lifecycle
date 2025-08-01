using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using CommLifecycle.Api.Services;
using SharedModels.DTOs;
using SharedModels.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CommLifecycleApiContext>(opt =>
    opt.UseInMemoryDatabase("Movies"));
builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.MapGet("/debug-route", () => Results.Ok("Routing is fine."));


using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<CommLifecycleApiContext>();
SeedDb s = new();
await s.Seed(context);

app.Run();
