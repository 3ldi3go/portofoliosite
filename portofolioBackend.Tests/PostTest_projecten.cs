using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;



public class PostTest_projecten : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;



    public PostTest_projecten(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    public async Task<string> GetTokenAsync()
    {
        var client = _factory.CreateClient();

        var loginDTO = new
        {
            Email = "bub41gawara@gmail.com",
            Password = "Bub41g@w4ra!"
        };

        var response = await client.PostAsJsonAsync("/login", loginDTO);
        var content = await response.Content.ReadAsStringAsync();
        var token = System.Text.Json.JsonDocument.Parse(content).RootElement.GetProperty("token").GetString();

        return token;
    }

    [Fact]

    public async Task fout_bij_POST_zonder_aanmelding()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/projecten",
        new ProjectenCreateDto
        {
            title = "Test Project",
            description = "Test Beschrijving",
            image = "https://test.com",
            link = "https://test.com/image.jpg",
            date = "2000-01-01"
        });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task fout_bij_POST_zonder_juiste_rol()
    {
        var client = _factory.CreateClient();

        var token = await GetTokenAsync();

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("/projecten",
        new ProjectenCreateDto
        {
            title = "Test Project",
            description = "Test Beschrijving",
            image = "https://test.com",
            link = "https://test.com/image.jpg",
            date = "2000-01-01"
        });
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}