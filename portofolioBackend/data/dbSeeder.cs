using Microsoft.AspNetCore.Identity;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();

        var context = scope.ServiceProvider
        .GetRequiredService<portofolioDB>();

        var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<User>>();

        var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

       string[] roles =
        {
            "Gast",
            "Editor",   
            "Admin"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

}