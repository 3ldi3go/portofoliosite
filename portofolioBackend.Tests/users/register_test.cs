using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;

public class registertest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public registertest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task fout_bij_registeren_met_bestaande_email()
    {
        var client = _factory.CreateClient();
        var scope = _factory.Services.CreateScope();

        var user = await TemporaryUser.CreateTemporaryUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
        try{
        var response = await client.PostAsJsonAsync("/register",
            new RegisterDTO
            {
                email = user.Email,
                password = "Test123!"
            });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        finally
        {   
            await scope.ServiceProvider.GetRequiredService<UserManager<User>>().DeleteAsync(user);
        }
    }

    [Fact]
    public async Task registratie_succesvol()
    {
        var client = _factory.CreateClient();
        var email = $"testuser_{Guid.NewGuid()}@example.com";
        try{
        var response = await client.PostAsJsonAsync("/register",
            new RegisterDTO
            {
                email = email,
                password = "Test123!"
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        finally
        {
            var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
        }
    }
}
            