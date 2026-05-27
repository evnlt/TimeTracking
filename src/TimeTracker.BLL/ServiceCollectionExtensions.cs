using Microsoft.Extensions.DependencyInjection;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Services;

namespace TimeTracker.BLL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddTransient<ICardService, CardService>();
        services.AddTransient<IHistoryService, HistoryService>();
        services.AddTransient<IWorkTimeService, WorkTimeService>();
        services.AddTransient<IStatisticsService, StatisticsService>();
        
        return services;
    }
}