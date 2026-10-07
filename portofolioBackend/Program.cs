var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPosts(); // jouw endpoints
app.Run();

public static class PostEndpoints
{
    public static RouteGroupBuilder MapPosts(
        this IEndpointRouteBuilder endpoints
    )
    {
        var posts = endpoints.MapGroup("/posts");
        posts.MapGet("/", () => "all");
        posts.MapGet("/{id}", (int id) => id);
        return posts;
    }
}

