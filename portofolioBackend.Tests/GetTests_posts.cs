using System.Net;
using Xunit;
using portofolioBackend;
using Microsoft.AspNetCore.Mvc.Testing;

namespace portofolioBackend.Tests;

public class GetTests_posts : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GetTests_posts(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }


    [Fact]
    public void Get_all_posts()
    {
        var client = _factory.CreateClient();

        var response = client.GetAsync("/posts").Result;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public void Get_post_by_id()
    {
        var client = _factory.CreateClient();

        var response = client.GetAsync("/posts/1").Result;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
        public void Content_not_empty()
    {
        var client = _factory.CreateClient();

        var response = client.GetAsync("/posts").Result;

        var content = response.Content.ReadAsStringAsync().Result;

        Assert.False(string.IsNullOrEmpty(content));
    }
}
