using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GitHub.Client;

public class ClientImplementation : IClientInterface // TODO: Rename this class to match the service it interacts with.
{
    private readonly HttpClient _client;
    private readonly ClientOptions _options;
    private readonly ILogger<ClientImplementation> _logger;

    public ClientImplementation(IHttpClientFactory factory, IOptions<ClientOptions> options, ILogger<ClientImplementation> logger)
    {
        _client = factory.CreateClient(ClientOptions.ClientName);
        _options = options.Value;
        _logger = logger;
    }
}
