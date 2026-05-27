using System.Text.Json.Serialization;
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
            .AddMessageQueue(globalSettings.RabbitMq);
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();