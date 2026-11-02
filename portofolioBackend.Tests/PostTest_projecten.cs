using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;

namespace portofolioBackend.Tests;

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

    var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

    return result!.Token;
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

    var loginDto = new LoginDTO
    {
        email= "toga_chan@gmail.com",
        password= "H1m1k()"
    };

    var loginResponse = await client.PostAsJsonAsync("/login", loginDto);


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

    [Fact]
    public async Task POST_met_juiste_rol()
    {
        var client = _factory.CreateClient();

            var loginDto = new LoginDTO
            {
                email = "dpmvanhagen@gmail.com",
                password = "Dpm18()9o6"
            };
  

        var loginResponse = await client.PostAsJsonAsync("/login", loginDto);

        var response = await client.PostAsJsonAsync("/projecten",
            new ProjectenCreateDto
            {
                title = "Test Project",
                description = "Test Beschrijving",
                image = "https://test.com",
                link = "https://test.com/image.jpg",
                date = "2000-01-01"
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}