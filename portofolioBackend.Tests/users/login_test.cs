using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;
public class logintest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public logintest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task login_succesvol()
    {
        var client = _factory.CreateClient();
        var scope = _factory.Services.CreateScope();

        var user = await TemporaryUser.CreateTemporaryUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
        try{
        var response = await client.PostAsJsonAsync("/login",
            new LoginDTO
            {
                email = user.Email,
                password = "Test123!"
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        finally
        {   
            await scope.ServiceProvider.GetRequiredService<UserManager<User>>().DeleteAsync(user);
        }
    }

    [Fact]
    public async Task login_gefaald()
    {
        var client = _factory.CreateClient();
        var scope = _factory.Services.CreateScope();

        var user = await TemporaryUser.CreateTemporaryUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
        try{
        var response = await client.PostAsJsonAsync("/login",
            new LoginDTO
            {
                email = user.Email,
                password = "DitKloptNiet!"
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        finally
        {   
            await scope.ServiceProvider.GetRequiredService<UserManager<User>>().DeleteAsync(user);
        }
    }
}