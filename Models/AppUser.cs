﻿
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class AppUser : IdentityUser
    {
        public User? User { get; set; }
    }
}
