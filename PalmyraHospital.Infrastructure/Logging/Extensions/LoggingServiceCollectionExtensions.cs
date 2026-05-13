using Microsoft.Extensions.DependencyInjection;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Implementations;
using PalmyraHospital.Infrastructure.Logging.Repositories;

namespace PalmyraHospital.Infrastructure.Logging.Extensions;

public static class LoggingServiceCollectionExtensions
{
    public static IServiceCollection AddLoggingInfrastructure(
        this IServiceCollection services)
    {
        // Core Logging
        services.AddScoped<ILogService, SerilogService>();

        // Security Logging
        services.AddScoped<
            ISecurityLogRepository,
            SecurityLogRepository>();

        services.AddScoped<
            ISecurityLogger,
            SecurityLogger>();

        services.AddScoped<
    IRequestTraceRepository,
    RequestTraceRepository>();

        services.AddScoped<
            IRequestTraceService,
            RequestTraceService>();

        return services;
    }
}