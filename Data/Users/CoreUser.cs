﻿
using Microsoft.AspNetCore.Identity;

public class CoreUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; } 
}