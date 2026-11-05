using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;

public class UpdateTest_posts : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public UpdateTest_posts(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task fout_bij_PUT_zonder_aanmelding()
    {
        var client = _factory.CreateClient();

        var response = await client.PutAsJsonAsync("/posts/1",
        new PostUpdateDto
        {
            title = "Test Post",
            description = "Test Beschrijving",
            date = "2000-01-01"
        });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task fout_bij_PUT_met_verkeerde_rol()
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

            var response = await client.PutAsJsonAsync("/posts/1",
                new PostUpdateDto
                {
                    title = "Test Post Updated",
                    description = "Test Beschrijving Updated",
                    date = "2000-01-02"
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
    public async Task PUT_met_juiste_rol()
    {
        var client = _factory.CreateClient();
        var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<portofolioDB>();
        var post = db.Posts.FirstOrDefault();

        var postId = post?.id;
        var originalTitle = post?.title;
        var originalDescription = post?.description;
        var originalDate = post?.date;

        var user = await TemporaryUser.CreateTemporaryAdmin(scope.ServiceProvider.GetRequiredService<UserManager<User>>(), "Admin");

        try
        {
            var loginDto = new LoginDTO
            {
                email = user.Email,
                password = "Test123!"
            };
            var loginResponse = await client.PostAsJsonAsync("/login", loginDto);
            
            var response = await client.PutAsJsonAsync($"/posts/{postId}",
                new PostUpdateDto
                {
                    title = "Test Post Updated",
                    description = "Test Beschrijving Updated",
                    date = "2000-01-02"
                });
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        finally
        {
            using var cleanupScope = _factory.Services.CreateScope();
            var cleanupDb = cleanupScope.ServiceProvider.GetRequiredService<portofolioDB>();
            var tPost = cleanupDb.Posts.FirstOrDefault(p => p.id == postId);
            if (tPost != null)
            {
                tPost.title = originalTitle;
                tPost.description = originalDescription;
                tPost.date = originalDate;
                await cleanupDb.SaveChangesAsync();
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            await userManager.DeleteAsync(user);
        }
    }
}