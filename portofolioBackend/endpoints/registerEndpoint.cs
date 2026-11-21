using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register",
        async (
            RegisterDTO dto,
            UserManager<User> userManager) =>
        {
            var user = new User
            {
            UserName = dto.email,
            Email = dto.email
            };

            var result = await userManager.CreateAsync(
            user,
            dto.password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors);
        }

        await userManager.AddToRoleAsync(
            user,
            "Gast");

        return Results.Ok("User registered successfully.");
        });
    }
}