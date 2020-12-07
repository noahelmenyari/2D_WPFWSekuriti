using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WPFWSecurity.Areas.Identity.Data;
using WPFWSecurity.Data;
using WPFWSecurity.Models;

namespace WPFWSecurity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WPFWSecurityContextA _context;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly UserManager<SchoolUser> _um;
        private readonly SignInManager<SchoolUser> _sm;


        public HomeController(SignInManager<SchoolUser> sm, ILogger<HomeController> logger, WPFWSecurityContextA context, RoleManager<IdentityRole> rm, UserManager<SchoolUser> um)
        {
            _context = context;
            _logger = logger;
            _rm = rm;
            _um = um;
            _sm = sm;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> RoleChange()
        {
            ClaimsPrincipal cp = HttpContext.User;
            SchoolUser user = await _um.GetUserAsync(cp);
            string rol = User.FindFirstValue(ClaimTypes.Role);
            if (rol == "Student")
            {
                await _um.RemoveFromRoleAsync(user, "Student");
                await _um.AddToRoleAsync(user, "Docent");
                await _context.SaveChangesAsync();
                await _sm.RefreshSignInAsync(user);
                
            }
            else if (rol == "Docent")
            {
                await _um.RemoveFromRoleAsync(user, "Docent");
                await _um.AddToRoleAsync(user, "Student");
                await _context.SaveChangesAsync();
                await _sm.RefreshSignInAsync(user);
            }
            return View("Index");
        }
    }
}
