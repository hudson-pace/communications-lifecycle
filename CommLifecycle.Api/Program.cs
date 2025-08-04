using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using CommLifecycle.Api.Services;
using SharedModels.DTOs;
using SharedModels.Models;
using System.Diagnostics;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var raw = builder.Configuration.GetConnectionString("CommLifecycleApiContext")!;

var password = Environment.GetEnvironmentVariable("DB_PASSWORD")!;
var connectionString = raw.Replace("${DB_PASSWORD}", password);

builder.Services.AddDbContextFactory<CommLifecycleApiContext>(options =>
    options.UseSqlServer(
        connectionString ??
        throw new InvalidOperationException("Connection string 'CommLifecycleApiContext' not found.")));

builder.Services.AddScoped<ICommunicationService, CommunicationService>();

var factory = new ConnectionFactory
{
    HostName = "host.docker.internal",
    UserName = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? string.Empty,
    Password = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? string.Empty,
};
var connection = await factory.CreateConnectionAsync();
builder.Services.AddSingleton(connection);
builder.Services.AddScoped<IRabbitPublisher, RabbitPublisher>();


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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CommLifecycleApiContext>();
    await context.Database.MigrateAsync();
    int communicationCount = await context.Communications.CountAsync();
    if (communicationCount == 0) await SeedDb.Seed(context);
}

app.Run();
