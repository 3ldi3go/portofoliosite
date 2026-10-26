using Microsoft.EntityFrameworkCore;

public static class ProjectenEndpoints
{
    public static void MapProjectenEndpoints(this IEndpointRouteBuilder app)
    {
        // GET all
        app.MapGet("/projecten", async (portofolioDB db) =>
            await db.Projecten.ToListAsync());

        // GET id
        app.MapGet("/projecten/{id}", async (int id, portofolioDB db) =>
            await db.Projecten.FindAsync(id) is Projecten project
                ? Results.Ok(project)
                : Results.NotFound());

        // POST
        app.MapPost("/projecten", async (ProjectenCreateDto dto, portofolioDB db) =>
        {
            var project = new Projecten
            {
                title = dto.title,
                description = dto.description,
                image = dto.image,
                link = dto.link,
                date = dto.date
            };

            db.Projecten.Add(project);
            await db.SaveChangesAsync();

            return Results.Created($"/projecten/{project.id}", project);
        });

        // PUT
        app.MapPut("/projecten/{id}", async (int id, ProjectenUpdateDto dto, portofolioDB db) =>
        {
            var project = await db.Projecten.FindAsync(id);

            if (project is null) return Results.NotFound();

            project.title = dto.title;
            project.description = dto.description;
            project.image = dto.image;
            project.link = dto.link;
            project.date = dto.date;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE
        app.MapDelete("/projecten/{id}", async (int id, portofolioDB db) =>
        {
            if (await db.Projecten.FindAsync(id) is Projecten project)
            {
                db.Projecten.Remove(project);
                await db.SaveChangesAsync();
                return Results.Ok(project); 
            }

            return Results.NotFound();
        });
    }
}
