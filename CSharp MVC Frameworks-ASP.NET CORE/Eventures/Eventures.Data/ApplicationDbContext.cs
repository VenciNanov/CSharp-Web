﻿using System;
using System.Collections.Generic;
using System.Text;
using Eventures.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
