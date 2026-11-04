using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace portofolioBackend.Tests;

public class UpdateTest_projecten : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    
    public UpdateTest_projecten(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task fout_bij_PUT_zonder_aanmelding()
    {
        var client = _factory.CreateClient();

        var response = await client.PutAsJsonAsync("/projecten/1",
        new ProjectenUpdateDto
        {
            title = "Test Project Updated",
            description = "Test Beschrijving Updated",
            image = "https://test.com/updated",
            link = "https://test.com/image_updated.jpg",
            date = "2000-01-02"
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

        var response = await client.PutAsJsonAsync("/projecten/1",
            new ProjectenUpdateDto
            {
                title = "Test Project",
                description = "Test Beschrijving",
                image = "https://test.com",
                link = "https://test.com/image.jpg",
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
    public async Task PUT_met_juiste_rol()
    {
        var client = _factory.CreateClient();
        var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<portofolioDB>();
        var project = db.Projecten.FirstOrDefault();

        Assert.NotNull(project); //safety precaution

        var projectId = project?.id;
        var originalTitle = project?.title;
        var originalDescription = project?.description;
        var originalImage = project?.image;
        var originalLink = project?.link;
        var originalDate = project?.date;

        var user = await TemporaryUser.CreateTemporaryAdmin(scope.ServiceProvider.GetRequiredService<UserManager<User>>(), "Admin");

        try
        {
            var loginDto = new LoginDTO
            {
                email = user.Email,
                password = "Test123!"
            };

            var loginResponse = await client.PostAsJsonAsync("/login", loginDto);

            var response = await client.PutAsJsonAsync($"/projecten/{projectId}",
                new ProjectenUpdateDto
                {
                    title = "Test Project",
                    description = "Test Beschrijving",
                    image = "https://test.com/",
                    link = "https://test.com/image.jpg",
                    date = "2000-01-03"
                });

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        finally
        {   
            using var cleanupScope = _factory.Services.CreateScope();
            var cleanupdb = cleanupScope.ServiceProvider.GetRequiredService<portofolioDB>();
            var tProject = cleanupdb.Projecten.FirstOrDefault(p => p.id == projectId);
            if (tProject != null)
            {
                tProject.title = originalTitle;
                tProject.description = originalDescription;
                tProject.image = originalImage;
                tProject.link = originalLink;
                tProject.date = originalDate;

                await cleanupdb.SaveChangesAsync();
            }
            
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await userManager.DeleteAsync(user);

        }
    }

    }