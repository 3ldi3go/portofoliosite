using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public int StudentNumber { get; set; }
}