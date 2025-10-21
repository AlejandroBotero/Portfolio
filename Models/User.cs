using Microsoft.AspNetCore.Identity;

namespace newmvc.Models;

public class User : IdentityUser
{
    public required string FullName { get; set; }

    // public required string Email { get; set; }

}