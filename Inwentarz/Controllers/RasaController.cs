using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inwentarz.Models;

namespace Inwentarz.Controllers
{
    public class RasaController : Controller
    {
        private readonly InwentarzDbContext _context;

        public RasaController(InwentarzDbContext context)
        {
            _context = context;
        }

        // GET: Rasa
        public async Task<IActionResult> Index(string sortOrder, string gatunekFilter)
        {
            ViewData["SortByName"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = gatunekFilter;

            var rasy = from r in _context.Rasa
                       select r;

            // Filtrowanie po gatunku
            if (!String.IsNullOrEmpty(gatunekFilter))
            {
                rasy = rasy.Where(r => r.Gatunek.Contains(gatunekFilter));
            }

            // Sortowanie
            rasy = sortOrder switch
            {
                "name_desc" => rasy.OrderByDescending(r => r.NazwaRasy),
                _ => rasy.OrderBy(r => r.NazwaRasy),
            };

            return View(await rasy.AsNoTracking().ToListAsync());
        }

        // GET: Rasa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rasa = await _context.Rasa.FirstOrDefaultAsync(m => m.RasyId == id);
            if (rasa == null) return NotFound();

            return View(rasa);
        }

        // GET: Rasa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rasa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RasyId,Gatunek,NazwaRasy,Opis")] Rasa rasa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rasa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rasa);
        }

        // GET: Rasa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rasa = await _context.Rasa.FindAsync(id);
            if (rasa == null) return NotFound();

            return View(rasa);
        }

        // POST: Rasa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RasyId,Gatunek,NazwaRasy,Opis")] Rasa rasa)
        {
            if (id != rasa.RasyId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rasa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RasaExists(rasa.RasyId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rasa);
        }

        // GET: Rasa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rasa = await _context.Rasa.FirstOrDefaultAsync(m => m.RasyId == id);
            if (rasa == null) return NotFound();

            return View(rasa);
        }

        // POST: Rasa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rasa = await _context.Rasa.FindAsync(id);
            if (rasa != null) _context.Rasa.Remove(rasa);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RasaExists(int id)
        {
            return _context.Rasa.Any(e => e.RasyId == id);
        }
    }
}
