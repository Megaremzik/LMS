﻿using Microsoft.AspNetCore.Identity;

namespace LMS.Entries
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
