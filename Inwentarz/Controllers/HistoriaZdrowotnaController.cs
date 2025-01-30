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
    public class HistoriaZdrowotnaController : Controller
    {
        private readonly InwentarzDbContext _context;

        public HistoriaZdrowotnaController(InwentarzDbContext context)
        {
            _context = context;
        }

        // GET: HistoriaZdrowotna
        public async Task<IActionResult> Index()
        {
            var inwentarzDbContext = _context.HistoriaZdrowotna.Include(h => h.Leczenie).Include(h => h.Zwierze);
            return View(await inwentarzDbContext.ToListAsync());
        }

        // GET: HistoriaZdrowotna/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaZdrowotna = await _context.HistoriaZdrowotna
                .Include(h => h.Leczenie)
                .Include(h => h.Zwierze)
                .FirstOrDefaultAsync(m => m.RekordId == id);
            if (historiaZdrowotna == null)
            {
                return NotFound();
            }

            return View(historiaZdrowotna);
        }

        // GET: HistoriaZdrowotna/Create
        public IActionResult Create()
        {
            ViewData["LeczenieId"] = new SelectList(_context.ZabiegWeterynaryjny, "LeczenieId", "LeczenieId");
            ViewData["IdentyfikatorZwierzecia"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie");
            return View();
        }

        // POST: HistoriaZdrowotna/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RekordId,IdentyfikatorZwierzecia,Diagnoza,DataDiagnozy,LeczenieId,Opis")] HistoriaZdrowotna historiaZdrowotna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historiaZdrowotna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeczenieId"] = new SelectList(_context.ZabiegWeterynaryjny, "LeczenieId", "LeczenieId", historiaZdrowotna.LeczenieId);
            ViewData["IdentyfikatorZwierzecia"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie", historiaZdrowotna.IdentyfikatorZwierzecia);
            return View(historiaZdrowotna);
        }

        // GET: HistoriaZdrowotna/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaZdrowotna = await _context.HistoriaZdrowotna.FindAsync(id);
            if (historiaZdrowotna == null)
            {
                return NotFound();
            }
            ViewData["LeczenieId"] = new SelectList(_context.ZabiegWeterynaryjny, "LeczenieId", "LeczenieId", historiaZdrowotna.LeczenieId);
            ViewData["IdentyfikatorZwierzecia"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie", historiaZdrowotna.IdentyfikatorZwierzecia);
            return View(historiaZdrowotna);
        }

        // POST: HistoriaZdrowotna/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RekordId,IdentyfikatorZwierzecia,Diagnoza,DataDiagnozy,LeczenieId,Opis")] HistoriaZdrowotna historiaZdrowotna)
        {
            if (id != historiaZdrowotna.RekordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historiaZdrowotna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriaZdrowotnaExists(historiaZdrowotna.RekordId))
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
            ViewData["LeczenieId"] = new SelectList(_context.ZabiegWeterynaryjny, "LeczenieId", "LeczenieId", historiaZdrowotna.LeczenieId);
            ViewData["IdentyfikatorZwierzecia"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie", historiaZdrowotna.IdentyfikatorZwierzecia);
            return View(historiaZdrowotna);
        }

        // GET: HistoriaZdrowotna/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaZdrowotna = await _context.HistoriaZdrowotna
                .Include(h => h.Leczenie)
                .Include(h => h.Zwierze)
                .FirstOrDefaultAsync(m => m.RekordId == id);
            if (historiaZdrowotna == null)
            {
                return NotFound();
            }

            return View(historiaZdrowotna);
        }

        // POST: HistoriaZdrowotna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historiaZdrowotna = await _context.HistoriaZdrowotna.FindAsync(id);
            if (historiaZdrowotna != null)
            {
                _context.HistoriaZdrowotna.Remove(historiaZdrowotna);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaZdrowotnaExists(int id)
        {
            return _context.HistoriaZdrowotna.Any(e => e.RekordId == id);
        }
    }
}
