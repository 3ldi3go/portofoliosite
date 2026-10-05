var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

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

