using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("portofolioDB");
builder.Services.AddDbContext<portofolioDB>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityApiEndpoints<User>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<portofolioDB>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
.AddPolicy("CanEdit", policy => policy.RequireRole("Admin", "Editor"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapRegisterEndpoint();
app.MapLoginEndpoint();
app.MapProjectenEndpoints();
app.MapPostEndpoints();

await DbSeeder.SeedAsync(app.Services);

app.Run();
