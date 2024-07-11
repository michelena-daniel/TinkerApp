using Microsoft.AspNetCore.Identity;

namespace TinkerAppProject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TinkerAppProjectUser class
public class TinkerAppProjectUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
}

