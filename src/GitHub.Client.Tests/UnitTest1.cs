using GitHub.Client;

namespace GitHub.Client.Tests;

[Collection("IntegrationTest")]
public class UnitTest1
{
    private readonly IClientInterface _client;

    public UnitTest1(TestFixture fixture)
    {
        _client = fixture.GetRequiredService<IClientInterface>();
    }

    [Fact]
    public void Test1()
    {
        // write tests here
    }
}
