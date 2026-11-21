using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login",
        async (
            LoginDTO dto,
            SignInManager<User> signInManager) =>
        {
            var result = await signInManager.PasswordSignInAsync(
                dto.email,
                dto.password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Results.BadRequest("Invalid login attempt.");
            }

            return Results.Ok("User logged in successfully.");
        });
    }
}