using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DetectiveAgencyProject.Models;

namespace Detective.Controllers
{
    public class DetectivesController : Controller
    {
        private readonly DetectiveAgencyDbContext _context;

        public DetectivesController(DetectiveAgencyDbContext context)
        {
            _context = context;
        }

        // GET: Detectives
        public async Task<IActionResult> Index()
        {
            return View(await _context.Detectives.ToListAsync());
        }

        // GET: Detectives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detective = await _context.Detectives
                .FirstOrDefaultAsync(m => m.DetectiveId == id);
            if (detective == null)
            {
                return NotFound();
            }

            return View(detective);
        }

        // GET: Detectives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Detectives/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetectiveId,Name,Experience,Specialization,AgencyId")] DetectiveAgencyProject.Models.Detective detective) // Fully qualified class name
        {
            if (ModelState.IsValid)
            {
                _context.Add(detective);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detective);
        }

        // GET: Detectives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detective = await _context.Detectives.FindAsync(id);
            if (detective == null)
            {
                return NotFound();
            }
            return View(detective);
        }

        // POST: Detectives/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetectiveId,Name,Experience,Specialization,AgencyId")] DetectiveAgencyProject.Models.Detective detective) // Fully qualified class name
        {
            if (id != detective.DetectiveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detective);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetectiveExists(detective.DetectiveId))
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
            return View(detective);
        }

        // GET: Detectives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detective = await _context.Detectives
                .FirstOrDefaultAsync(m => m.DetectiveId == id);
            if (detective == null)
            {
                return NotFound();
            }

            return View(detective);
        }

        // POST: Detectives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detective = await _context.Detectives.FindAsync(id);
            if (detective != null)
            {
                _context.Detectives.Remove(detective);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetectiveExists(int id)
        {
            return _context.Detectives.Any(e => e.DetectiveId == id);
        }
    }
}
