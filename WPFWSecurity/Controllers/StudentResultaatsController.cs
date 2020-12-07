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

        // GET: StudentResultaats/Create
        [Authorize(Roles = "Docent")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentResultaats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Docent")]
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

        // GET: StudentResultaats/Edit/5
        [Authorize(Roles = "Docent")]
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
        // POST: StudentResultaats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Docent")]
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
        [Authorize(Roles = "Docent")]
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
        [Authorize(Roles = "Docent")]
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

        public IActionResult Zoek(String query)
        {
            if(query != null)
            {
                if(int.TryParse(query, out _)) 
                {
                    //_context.StudentResultaat.Where(r => r.Cijfer.Equals(q))
                    var res = _context.StudentResultaat.FromSqlRaw("select * from StudentResultaat where cijfer like '" + query + "'");
                    _context.SaveChanges();
                    return View(new Resultaten(query, res));
                }
                else
                {
                    return View(new Resultaten(query, _context.StudentResultaat.Where(r => r.StudentNaam.Contains(query))));
                }
                
            }

            return View();
        }

        public class Resultaten
        {
            public string Query { get; set; }
            public IEnumerable<StudentResultaat> Sresultaten { get; set; }
            public Resultaten(string query, IEnumerable<StudentResultaat> sresultaten)
            {
                Query = query;
                Sresultaten = sresultaten;
            }
        }
    }
}
