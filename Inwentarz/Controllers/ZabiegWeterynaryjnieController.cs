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
    public class ZabiegWeterynaryjnieController : Controller
    {
        private readonly InwentarzDbContext _context;

        public ZabiegWeterynaryjnieController(InwentarzDbContext context)
        {
            _context = context;
        }

        // GET: ZabiegWeterynaryjnie
        public async Task<IActionResult> Index()
        {
            var inwentarzDbContext = _context.ZabiegWeterynaryjny.Include(z => z.Zwierze);
            return View(await inwentarzDbContext.ToListAsync());
        }

        // GET: ZabiegWeterynaryjnie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zabiegWeterynaryjny = await _context.ZabiegWeterynaryjny
                .Include(z => z.Zwierze)
                .FirstOrDefaultAsync(m => m.LeczenieId == id);
            if (zabiegWeterynaryjny == null)
            {
                return NotFound();
            }

            return View(zabiegWeterynaryjny);
        }

        // GET: ZabiegWeterynaryjnie/Create
        public IActionResult Create()
        {
            ViewData["ZwierzeId"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie");
            return View();
        }

        // POST: ZabiegWeterynaryjnie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeczenieId,ZwierzeId,Data,RodzajStworzenia,Opis,Weterynarz")] ZabiegWeterynaryjny zabiegWeterynaryjny)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zabiegWeterynaryjny);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZwierzeId"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie", zabiegWeterynaryjny.ZwierzeId);
            return View(zabiegWeterynaryjny);
        }

        // GET: ZabiegWeterynaryjnie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zabiegWeterynaryjny = await _context.ZabiegWeterynaryjny.FindAsync(id);
            if (zabiegWeterynaryjny == null)
            {
                return NotFound();
            }
            ViewData["ZwierzeId"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie", zabiegWeterynaryjny.ZwierzeId);
            return View(zabiegWeterynaryjny);
        }

        // POST: ZabiegWeterynaryjnie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeczenieId,ZwierzeId,Data,RodzajStworzenia,Opis,Weterynarz")] ZabiegWeterynaryjny zabiegWeterynaryjny)
        {
            if (id != zabiegWeterynaryjny.LeczenieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zabiegWeterynaryjny);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZabiegWeterynaryjnyExists(zabiegWeterynaryjny.LeczenieId))
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
            ViewData["ZwierzeId"] = new SelectList(_context.Zwierze, "ZwierzeId", "Imie", zabiegWeterynaryjny.ZwierzeId);
            return View(zabiegWeterynaryjny);
        }

        // GET: ZabiegWeterynaryjnie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zabiegWeterynaryjny = await _context.ZabiegWeterynaryjny
                .Include(z => z.Zwierze)
                .FirstOrDefaultAsync(m => m.LeczenieId == id);
            if (zabiegWeterynaryjny == null)
            {
                return NotFound();
            }

            return View(zabiegWeterynaryjny);
        }

        // POST: ZabiegWeterynaryjnie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zabiegWeterynaryjny = await _context.ZabiegWeterynaryjny.FindAsync(id);
            if (zabiegWeterynaryjny != null)
            {
                _context.ZabiegWeterynaryjny.Remove(zabiegWeterynaryjny);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZabiegWeterynaryjnyExists(int id)
        {
            return _context.ZabiegWeterynaryjny.Any(e => e.LeczenieId == id);
        }
    }
}
