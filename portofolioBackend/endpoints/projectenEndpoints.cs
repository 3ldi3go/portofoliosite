using Microsoft.EntityFrameworkCore;
public static class ProjectenEndpoints
{
    public static void MapProjectenEndpoints(this IEndpointRouteBuilder app)
    {
    app.MapGet("/projecten" , async (portofolioDB db) => await db.Projecten.ToListAsync());
    app.MapGet("/projecten/{id}", async (int id, portofolioDB db) => 
    await db.Projecten.FindAsync(id) is Projecten project 
        ? Results.Ok(project) 
        : Results.NotFound());
app.MapPost("/projecten", async (Projecten project, portofolioDB db) => 
{
    db.Projecten.Add(project);
    await db.SaveChangesAsync();
    return Results.Created($"/projecten/{project.id}", project);
});
app.MapPut("/projecten/{id}", async (int id, Projecten inputProject, portofolioDB db) => 
{
    var project = await db.Projecten.FindAsync(id);

    if (project is null) return Results.NotFound();

    project.title = inputProject.title;
    project.description = inputProject.description;
    project.image = inputProject.image;
    project.link = inputProject.link;
    project.date = inputProject.date;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
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