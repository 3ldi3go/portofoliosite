using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;

public class PostTest_posts : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;



    public PostTest_posts(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]

    public async Task fout_bij_POST_zonder_aanmelding()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/posts",
        new PostCreateDto
        {
            title = "Test Post",
            description = "Test Beschrijving",
            date = "2000-01-01"
        });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    
[Fact]
public async Task fout_bij_POST_zonder_juiste_rol()
{
    var client = _factory.CreateClient();
    var scope = _factory.Services.CreateScope();

    var user = await TemporaryUser.CreateTemporaryUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());

    try
    {
        var loginDto = new LoginDTO
        {
            email = user.Email,
            password = "Test123!"
        };

        var loginResponse = await client.PostAsJsonAsync("/login", loginDto);

        var response = await client.PostAsJsonAsync("/posts",
            new PostCreateDto
            {
            title = "Test Post",
            description = "Test Beschrijving",
            date = "2000-01-01"
        });    

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    finally
    {   
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await userManager.DeleteAsync(user);

    }
}

[Fact]
public async Task POST_met_juiste_rol()
{
    var client = _factory.CreateClient();

    var scope = _factory.Services.CreateScope();

    var user = await TemporaryUser.CreateTemporaryAdmin(
        scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
        "Admin");

    try
    {
        var loginDto = new LoginDTO
        {
            email = user.Email,
            password = "Test123!"
        };

        var loginResponse = await client.PostAsJsonAsync("/login", loginDto);

        var response = await client.PostAsJsonAsync("/posts",
            new PostCreateDto
            {
            title = "Test Post",
            description = "Test Beschrijving",
            date = "2000-01-01"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    finally
    {
        var db = scope.ServiceProvider
        .GetRequiredService<portofolioDB>();

        var post = db.Posts
        .FirstOrDefault(p => p.title == "Test Post");

        if (post != null)
        {
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
        }
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<User>>();

        await userManager.DeleteAsync(user);
    }
}


}