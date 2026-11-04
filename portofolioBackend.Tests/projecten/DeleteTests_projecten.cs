using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;

public class DeleteTests_projecten : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public DeleteTests_projecten(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task fout_bij_DELETE_zonder_aanmelding()
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync("/projecten/1");

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

            var response = await client.DeleteAsync("/projecten/1");

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
        var project = db.Projecten.FirstOrDefault();

        Assert.NotNull(project); //safety precaution

        var projectId = project?.id;

        var user = await TemporaryUser.CreateTemporaryAdmin(scope.ServiceProvider.GetRequiredService<UserManager<User>>(), "Admin");

        try
        {
            var loginDto = new LoginDTO
            {
                email = user.Email,
                password = "Test123!"
            };

            var loginResponse = await client.PostAsJsonAsync("/login", loginDto);
            await client.DeleteAsync($"/projecten/{projectId}");
            var checkDeletedProject = await client.GetAsync($"/projecten/{projectId}");

            Assert.Equal(HttpStatusCode.NotFound, checkDeletedProject.StatusCode);
        }
        finally
        {   
            var cleanupscope = _factory.Services.CreateScope();
            var cleanupdb = cleanupscope.ServiceProvider.GetRequiredService<portofolioDB>();

            await cleanupdb.Projecten.AddAsync(new Projecten
            {
                title = project?.title,
                description = project?.description,
                image = project?.image,
                link = project?.link,
                date = project?.date
            });

            await cleanupdb.SaveChangesAsync();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            await userManager.DeleteAsync(user);
        }
    }
}