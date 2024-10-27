using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;

namespace LeadLeague.Tests;

/// <summary>
/// Enable obtaining a token from Auth0 using the client credentials grant type.
/// </summary>
public class M2MToken(ITestOutputHelper output)
{
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private record RequestBody(string ClientId, string ClientSecret, string Audience, string GrantType);

    private record ResponseBody(string AccessToken, string TokenType);


    [Fact]
    public async Task GetToken()
    {
        var client = new HttpClient();

        var myAppConfig = TestConfigHelper.GetIConfigurationRoot();

        var clientId = myAppConfig["OktaClientId"]!;
        var clientSecret = myAppConfig["OktaClientSecret"]!;
        var audience = myAppConfig["OktaAudience"]!;

        var contentParameters = new RequestBody(
            clientId,
            clientSecret,
            audience,
            "client_credentials");

        var contentJson = JsonSerializer.Serialize(contentParameters, _serializerOptions);

        var message = new StringContent(contentJson, new MediaTypeHeaderValue("application/json"));

        var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-cisfi8233px4sz8d.eu.auth0.com/oauth/token")
        {
            Content = message
        };

        var result = await client.SendAsync(request);
        var responseContent = await result.Content.ReadAsStringAsync();
        var responseBody = JsonSerializer.Deserialize<ResponseBody>(responseContent, _serializerOptions);
        output.WriteLine(responseBody?.AccessToken ?? "null", OutputLevel.Information);
    }
}