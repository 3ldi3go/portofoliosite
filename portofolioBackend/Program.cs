using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<portofolioDB>(opt => opt.UseInMemoryDatabase("portfolio"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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


app.Run();

