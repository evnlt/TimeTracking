using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Stores;

namespace TimeTracker.DAL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString, int commandTimeout)
    {
        services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(connectionString,
                optionsBuilder => optionsBuilder.EnableRetryOnFailure()
                    .CommandTimeout(commandTimeout))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging());

        services.AddTransient<IAttendanceStore, AttendanceStore>();
        services.AddTransient<ICardStore, CardStore>();

        return services;
    }
}