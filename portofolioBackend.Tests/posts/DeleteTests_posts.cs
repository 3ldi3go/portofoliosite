using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;

public class DeleteTests_posts : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public DeleteTests_posts(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task fout_bij_DELETE_zonder_aanmelding()
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync("/posts/1");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task fout_bij_DELETE_met_verkeerde_rol()
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

            var response = await client.DeleteAsync("/posts/1");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
        finally
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            await userManager.DeleteAsync(user);
        }
    }

    [Fact]
    public async Task DELETE_met_juiste_rol()
    {
        var client = _factory.CreateClient();
        var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<portofolioDB>();
        var post = db.Posts.FirstOrDefault();

        var postId = post?.id;

        var user = await TemporaryUser.CreateTemporaryAdmin(scope.ServiceProvider.GetRequiredService<UserManager<User>>(), "Admin");

        try
        {
            var loginDto = new LoginDTO
            {
                email = user.Email,
                password = "Test123!"
            };
            var loginResponse = await client.PostAsJsonAsync("/login", loginDto);
            await client.DeleteAsync($"/posts/{postId}");
            var checkDeletedPost = await client.GetAsync($"/posts/{postId}");

            Assert.Equal(HttpStatusCode.NotFound, checkDeletedPost.StatusCode);
        }
        finally
        {
            var cleanupScope = _factory.Services.CreateScope();
            var dbCleanup = cleanupScope.ServiceProvider.GetRequiredService<portofolioDB>();

            await dbCleanup.Posts.AddAsync(new Posts
            {
                title = post?.title,
                description = post?.description,
                date = post?.date
            });

            await dbCleanup.SaveChangesAsync();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            await userManager.DeleteAsync(user);
        }
    }
}