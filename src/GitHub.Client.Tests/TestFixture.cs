using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GitHub.Client.Tests;

/// <summary>
/// Test fixture for client integration tests.
/// </summary>
public class TestFixture
{
    private readonly IServiceProvider _provider;

    public TestFixture()
    {
        // Setup configuration
        var configuration = new ConfigurationManager();
        configuration.AddJsonFile("appsettings.json", optional: false);

        // Setup services and register loggers
        var services = new ServiceCollection();
        services.AddLogging(options =>
        {
            options.AddDebug();
            options.AddConsole();
        });

        // Register client services
        services.AddClient(configuration);

        // Build service provider
        _provider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Get service of type T from the IServiceProvider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>A service object of type T.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T GetRequiredService<T>() where T : notnull => _provider.GetRequiredService<T>();
}
