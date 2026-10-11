using Microsoft.EntityFrameworkCore;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/posts" , async (portofolioDB db) => await db.Posts.ToListAsync());
app.MapGet("/posts/{id}", async (int id, portofolioDB db) => 
    await db.Posts.FindAsync(id) is Posts post 
        ? Results.Ok(post) 
        : Results.NotFound());
app.MapPost("/posts", async (Posts post, portofolioDB db) => 
{
    db.Posts.Add(post);
    await db.SaveChangesAsync();
    return Results.Created($"/posts/{post.id}", post);
});
app.MapPut("/posts/{id}", async (int id, Posts inputPost, portofolioDB db) => 
{
    var post = await db.Posts.FindAsync(id);

    if (post is null) return Results.NotFound();

    post.title = inputPost.title;
    post.description = inputPost.description;
    post.date = inputPost.date;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/posts/{id}", async (int id, portofolioDB db) => 
{
    if (await db.Posts.FindAsync(id) is Posts post)
    {
        db.Posts.Remove(post);
        await db.SaveChangesAsync();
        return Results.Ok(post);
    }

    return Results.NotFound();
});
    }
}