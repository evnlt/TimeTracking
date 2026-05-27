using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BLL.Abstraction;
using TimeTracker.MessageQueue.Abstractions;
using TimeTracker.MessageQueue.Services;
using TimeTracker.MessageQueue.Settings;

namespace TimeTracker.MessageQueue;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageQueue(
        this IServiceCollection services,
        RabbitMqSettings settings)
    {
        services.AddSingleton(settings);

        services.AddSingleton<IAttendanceEventService,
            AttendanceEventService>();

        services.AddSingleton<CardTouchedConsumer>();
        
        services.AddTransient<ICardTouchEventProcessor, CardTouchEventProcessor>();

        services.AddHostedService<CardTouchedBackgroundService>();

        return services;
    }
}