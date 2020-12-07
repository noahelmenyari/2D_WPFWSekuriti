using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPFWSecurity.Areas.Identity.Data;
using WPFWSecurity.Data;

[assembly: HostingStartup(typeof(WPFWSecurity.Areas.Identity.IdentityHostingStartup))]
namespace WPFWSecurity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WPFWSecurityContextA>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("WPFWSecurityContextAConnection")));

                services.AddIdentity<SchoolUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<WPFWSecurityContextA>()
                    .AddRoles<IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<WPFWSecurityContextA>();
            });
        }
    }
}