using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WPFWSecurity.Data;
using WPFWSecurity.Models;

namespace WPFWSecurity.Controllers
{
    public class StudentResultaatsController : Controller
    {
        private readonly WPFWSecurityContextA _context;

        private readonly RoleManager<IdentityRole> rm;

        public StudentResultaatsController(WPFWSecurityContextA context)
        {
            _context = context;
        }

        // GET: StudentResultaats
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentResultaat.ToListAsync());
        }

        // GET: StudentResultaats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentResultaat = await _context.StudentResultaat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentResultaat == null)
            {
                return NotFound();
            }

            return View(studentResultaat);
        }
        [Authorize(Roles = "Docent")]
        // GET: StudentResultaats/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Docent")]
        // POST: StudentResultaats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentNaam,Cijfer")] StudentResultaat studentResultaat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentResultaat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentResultaat);
        }
        [Authorize(Roles = "Docent")]
        // GET: StudentResultaats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentResultaat = await _context.StudentResultaat.FindAsync(id);
            if (studentResultaat == null)
            {
                return NotFound();
            }
            return View(studentResultaat);
        }
        [Authorize(Roles = "Docent")]
        // POST: StudentResultaats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentNaam,Cijfer")] StudentResultaat studentResultaat)
        {
            if (id != studentResultaat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentResultaat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentResultaatExists(studentResultaat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentResultaat);
        }

        // GET: StudentResultaats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentResultaat = await _context.StudentResultaat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentResultaat == null)
            {
                return NotFound();
            }

            return View(studentResultaat);
        }

        // POST: StudentResultaats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentResultaat = await _context.StudentResultaat.FindAsync(id);
            _context.StudentResultaat.Remove(studentResultaat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentResultaatExists(int id)
        {
            return _context.StudentResultaat.Any(e => e.Id == id);
        }
    }
}
