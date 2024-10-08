﻿using Microsoft.AspNetCore.Identity;
using TinkerAppProject.Models.Expenses;

namespace TinkerAppProject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TinkerAppProjectUser class
public class TinkerAppProjectUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
    public List<ExpenseModel> Expenses { get; set; } = [];
}

