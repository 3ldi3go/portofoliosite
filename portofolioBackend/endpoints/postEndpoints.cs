using Microsoft.EntityFrameworkCore;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        // GET all
        app.MapGet("/posts", async (portofolioDB db) =>
            await db.Posts.ToListAsync());

        // GET ID
        app.MapGet("/posts/{id}", async (int id, portofolioDB db) =>
            await db.Posts.FindAsync(id) is Posts post
                ? Results.Ok(post)
                : Results.NotFound());

        // POST 
        app.MapPost("/posts", async (PostCreateDto dto, portofolioDB db) =>
        {
            var post = new Posts
            {
                title = dto.title,
                description = dto.description,
                date = dto.date
            };

            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return Results.Created($"/posts/{post.id}", post);
        }).RequireAuthorization("CanEdit");

        // PUT 
        app.MapPut("/posts/{id}", async (int id, PostUpdateDto dto, portofolioDB db) =>
        {
            var post = await db.Posts.FindAsync(id);

            if (post is null) return Results.NotFound();

            post.title = dto.title;
            post.description = dto.description;
            post.date = dto.date;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }).RequireAuthorization("CanEdit");

        // DELETE 
        app.MapDelete("/posts/{id}", async (int id, portofolioDB db) =>
        {
            if (await db.Posts.FindAsync(id) is Posts post)
            {
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
                return Results.Ok(post);
            }

            return Results.NotFound();
        }).RequireAuthorization("CanEdit");
    }
}
