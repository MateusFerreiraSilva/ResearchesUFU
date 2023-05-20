using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ResearchesUFU.IntegrationTests;

public class IntegrationTestBase
{
    protected readonly HttpClient TestClient;

    public IntegrationTestBase()
    {
        var appFactory = new  WebApplicationFactory<Program>();
        TestClient = appFactory.CreateClient();
    }
}