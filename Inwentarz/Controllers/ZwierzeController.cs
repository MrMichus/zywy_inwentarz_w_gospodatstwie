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
    public class ZwierzeController : Controller
    {
        private readonly InwentarzDbContext _context;

        public ZwierzeController(InwentarzDbContext context)
        {
            _context = context;
        }

        // GET: Zwierze
        public async Task<IActionResult> Index(string gatunekFilter, string sortOrder)
        {
            var zwierzeta = _context.Zwierze
                .Include(z => z.Pracownik)
                .Include(z => z.RasaObj)
                .AsQueryable();

            // 📌 FILTROWANIE według gatunku
            if (!string.IsNullOrEmpty(gatunekFilter))
            {
                zwierzeta = zwierzeta.Where(z => z.Gatunek.Contains(gatunekFilter));
            }

            // 📌 SORTOWANIE
            ViewData["SortByName"] = sortOrder == "imie_desc" ? "imie_asc" : "imie_desc";
            ViewData["SortByBirthDate"] = sortOrder == "data_asc" ? "data_desc" : "data_asc";

            switch (sortOrder)
            {
                case "imie_desc":
                    zwierzeta = zwierzeta.OrderByDescending(z => z.Imie);
                    break;
                case "data_asc":
                    zwierzeta = zwierzeta.OrderBy(z => z.DataUrodzenia);
                    break;
                case "data_desc":
                    zwierzeta = zwierzeta.OrderByDescending(z => z.DataUrodzenia);
                    break;
                default:
                    zwierzeta = zwierzeta.OrderBy(z => z.Imie); // Domyślnie sortuj po imieniu A-Z
                    break;
            }

            return View(await zwierzeta.ToListAsync());
        }

        // GET: Zwierze/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zwierze = await _context.Zwierze
                .Include(z => z.Pracownik)
                .Include(z => z.RasaObj)
                .FirstOrDefaultAsync(m => m.ZwierzeId == id);
            if (zwierze == null)
            {
                return NotFound();
            }

            return View(zwierze);
        }

        // GET: Zwierze/Create
        public IActionResult Create()
        {
            ViewData["OpiekunId"] = new SelectList(_context.Pracownik, "IdPracownika", "Imie");
            ViewData["Rasa"] = new SelectList(_context.Rasa, "RasyId", "Gatunek");
            return View();
        }

        // POST: Zwierze/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZwierzeId,Imie,Gatunek,Rasa,DataUrodzenia,Plec,PrzyjazdData,Waga,StatusZdrowotny,OpiekunId")] Zwierze zwierze)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zwierze);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OpiekunId"] = new SelectList(_context.Pracownik, "IdPracownika", "Imie", zwierze.OpiekunId);
            ViewData["Rasa"] = new SelectList(_context.Rasa, "RasyId", "Gatunek", zwierze.Rasa);
            return View(zwierze);
        }

        // GET: Zwierze/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zwierze = await _context.Zwierze.FindAsync(id);
            if (zwierze == null)
            {
                return NotFound();
            }
            ViewData["OpiekunId"] = new SelectList(_context.Pracownik, "IdPracownika", "Imie", zwierze.OpiekunId);
            ViewData["Rasa"] = new SelectList(_context.Rasa, "RasyId", "Gatunek", zwierze.Rasa);
            return View(zwierze);
        }

        // POST: Zwierze/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZwierzeId,Imie,Gatunek,Rasa,DataUrodzenia,Plec,PrzyjazdData,Waga,StatusZdrowotny,OpiekunId")] Zwierze zwierze)
        {
            if (id != zwierze.ZwierzeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zwierze);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZwierzeExists(zwierze.ZwierzeId))
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
            ViewData["OpiekunId"] = new SelectList(_context.Pracownik, "IdPracownika", "Imie", zwierze.OpiekunId);
            ViewData["Rasa"] = new SelectList(_context.Rasa, "RasyId", "Gatunek", zwierze.Rasa);
            return View(zwierze);
        }

        // GET: Zwierze/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zwierze = await _context.Zwierze
                .Include(z => z.Pracownik)
                .Include(z => z.RasaObj)
                .FirstOrDefaultAsync(m => m.ZwierzeId == id);
            if (zwierze == null)
            {
                return NotFound();
            }

            return View(zwierze);
        }

        // POST: Zwierze/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zwierze = await _context.Zwierze.FindAsync(id);
            if (zwierze != null)
            {
                _context.Zwierze.Remove(zwierze);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZwierzeExists(int id)
        {
            return _context.Zwierze.Any(e => e.ZwierzeId == id);
        }
    }
}
