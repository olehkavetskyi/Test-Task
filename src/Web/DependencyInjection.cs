using AspNetCoreRateLimit;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration config)
    {
        var ipRateLimitOptions = config.GetSection("IpRateLimiting").Get<IpRateLimitOptions>();

        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = ipRateLimitOptions!.EnableEndpointRateLimiting;
            options.StackBlockedRequests = ipRateLimitOptions.StackBlockedRequests;
            options.HttpStatusCode = ipRateLimitOptions.HttpStatusCode;
            options.RealIpHeader = ipRateLimitOptions.RealIpHeader;
            options.ClientIdHeader = ipRateLimitOptions.ClientIdHeader;
            options.GeneralRules = ipRateLimitOptions.GeneralRules;
        });

        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        services.AddInMemoryRateLimiting();

        return services;
    }
}
