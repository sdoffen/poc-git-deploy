using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace GitHub.Client;

[ExcludeFromCodeCoverage]
public static class CompositionExtensions
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {
        // Check for the marker service to determine if the client has already been registered.
        if (services.Any(x => x.ServiceType == typeof(ClientMarker)))
            return services;

        // Register the client options using the configuration.
        services.Configure<ClientOptions>(configuration.GetSection(ClientOptions.SectionName));
        var options = configuration
            .GetSection(ClientOptions.SectionName)
            .Get<ClientOptions>()
            ?? new ClientOptions();

        // Register the client with the retry policy.
        services.AddHttpClient(ClientOptions.ClientName, client =>
        {
            client.BaseAddress = options.ClientUrl;
        }).AddPolicyHandler(ConfigureRetryPolicy(options));

        // Register the client interface.
        services.AddScoped<IClientInterface, ClientImplementation>();

        // Register the marker service to ensure the client is only registered once.
        services.AddSingleton<ClientMarker>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> ConfigureRetryPolicy(ClientOptions options)
    {
        TimeSpan SleepDurationProvider(int retryAttempt) =>
            TimeSpan.FromMilliseconds(Math.Pow(options.RetryDelayMilliseconds, retryAttempt));

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                options.RetryCount,
                SleepDurationProvider
            );
    }
}
