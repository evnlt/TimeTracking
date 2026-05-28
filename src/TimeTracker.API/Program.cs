using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Settings;
using TimeTracker.BLL;
using TimeTracker.DAL;
using TimeTracker.MessageQueue;

var builder = WebApplication.CreateBuilder(args);

builder
    .Host
    .ConfigureServices((context, collection) =>
    {
        var globalSettings = context.Configuration.Get<GlobalSettings>()!;

        collection
            .AddBusinessLogicLayer()
            .AddDataAccessLayer(
                globalSettings.ConnectionStrings.DefaultConnection,
                globalSettings.ConnectionStrings.CommandTimeout)
            .AddMessageQueue(globalSettings.RabbitMqSettings);
    });

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
        options.Filters.Add(new ConsumesAttribute("application/json"));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();