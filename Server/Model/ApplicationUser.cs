using Microsoft.AspNetCore.Identity;

namespace Server.Model;

public class ApplicationUser : IdentityUser
{
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
}