using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Services;
using TimeTracker.BLL.Validators;

namespace TimeTracker.BLL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddTransient<ICardService, CardService>();
        services.AddTransient<IHistoryService, HistoryService>();
        services.AddTransient<IWorkTimeService, WorkTimeService>();
        services.AddTransient<IStatisticsService, StatisticsService>();
        
        services.AddTransient<CardValidator>();
        services.AddTransient<HistoryValidator>();
        services.AddTransient<StatisticsValidator>();
        services.AddTransient<WorkTimeValidator>();
        
        return services;
    }
}