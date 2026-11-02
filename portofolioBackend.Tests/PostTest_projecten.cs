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

    
    //omdat login geen token meegeeft werkt deze test niet. Ik heb op het moment niet de kennis om dit te fixen
    [Fact]
    public async Task fout_bij_POST_zonder_juiste_rol()
    {
    var client = _factory.CreateClient();

    var loginDto = new
    {
        email = "bub41gawara@gmail.com",
        password = "Bub41g@w4ra"
    };

    var loginResponse = await client.PostAsJsonAsync("/login", loginDto);

    Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

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