using Microsoft.AspNetCore.Identity;
using portofolioBackend;


public class TemporaryUser
    {
    public static async Task<User> CreateTemporaryAdmin(UserManager<User> userManager, string role)
    {

    var email = $"test{Guid.NewGuid()}@test.nl";
    var user = new User
    
    {
        Email = email,
        UserName = email
    };

    await userManager.CreateAsync(user, "Test123!");
    await userManager.AddToRoleAsync(user, role);

    return user;
    }
    

    public static async Task<User> CreateTemporaryUser(UserManager<User> userManager)
    {
         var email = $"test{Guid.NewGuid()}@test.nl";
        var user = new User
        {
            Email = email,
            UserName = email
        };

        await userManager.CreateAsync(user, "Test123!");

        return user;
    }
}

