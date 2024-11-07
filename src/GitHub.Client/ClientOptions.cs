using System.Diagnostics.CodeAnalysis;

namespace GitHub.Client;

/// <summary>
/// Options used for configuring the client.
/// </summary>
[ExcludeFromCodeCoverage]
public class ClientOptions
{
    /// <summary>
    /// The value used to create named <see cref="HttpClient"/> instances.
    /// </summary>
    internal static readonly string ClientName = Guid.NewGuid().ToString();

    /// <summary>
    /// The configuration section name to deserialize the options from.
    /// </summary>
    public static readonly string SectionName = "Client"; // TODO: Rename this section to be more specific to the client.

    /// <summary>
    /// The base address of the client.
    /// </summary>
    public Uri ClientUrl { get; set; } = null!;

    /// <summary>
    /// The maximum attempted retries for calls to the client.
    /// </summary>
    /// <remarks>Default value is 3.</remarks>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// Base value for the retry duration. The actual duration is calculated using this value and an exponential backoff strategy.
    /// </summary>
    /// <remarks>Default value is 2000.</remarks>
    public int RetryDelayMilliseconds { get; set; } = 2000;
}
