using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WPFWSecurity.Areas.Identity.Data;

namespace WPFWSecurity.Data
{
    public class WPFWSecurityContextA : IdentityDbContext<SchoolUser>
    {
        public WPFWSecurityContextA(DbContextOptions<WPFWSecurityContextA> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<WPFWSecurity.Models.StudentResultaat> StudentResultaat { get; set; }

    }
}
