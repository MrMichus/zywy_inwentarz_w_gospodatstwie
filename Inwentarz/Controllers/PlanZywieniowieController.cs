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
    public class PlanZywieniowieController : Controller
    {
        private readonly InwentarzDbContext _context;

        public PlanZywieniowieController(InwentarzDbContext context)
        {
            _context = context;
        }

        // GET: PlanZywieniowie
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanZywieniowy.ToListAsync());
        }

        // GET: PlanZywieniowie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planZywieniowy = await _context.PlanZywieniowy
                .FirstOrDefaultAsync(m => m.KarmienieId == id);
            if (planZywieniowy == null)
            {
                return NotFound();
            }

            return View(planZywieniowy);
        }

        // GET: PlanZywieniowie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanZywieniowie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KarmienieId,PoraKarmienia,RodzajPaszy,Ilosc")] PlanZywieniowy planZywieniowy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planZywieniowy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planZywieniowy);
        }

        // GET: PlanZywieniowie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planZywieniowy = await _context.PlanZywieniowy.FindAsync(id);
            if (planZywieniowy == null)
            {
                return NotFound();
            }
            return View(planZywieniowy);
        }

        // POST: PlanZywieniowie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KarmienieId,PoraKarmienia,RodzajPaszy,Ilosc")] PlanZywieniowy planZywieniowy)
        {
            if (id != planZywieniowy.KarmienieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planZywieniowy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanZywieniowyExists(planZywieniowy.KarmienieId))
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
            return View(planZywieniowy);
        }

        // GET: PlanZywieniowie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planZywieniowy = await _context.PlanZywieniowy
                .FirstOrDefaultAsync(m => m.KarmienieId == id);
            if (planZywieniowy == null)
            {
                return NotFound();
            }

            return View(planZywieniowy);
        }

        // POST: PlanZywieniowie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planZywieniowy = await _context.PlanZywieniowy.FindAsync(id);
            if (planZywieniowy != null)
            {
                _context.PlanZywieniowy.Remove(planZywieniowy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanZywieniowyExists(int id)
        {
            return _context.PlanZywieniowy.Any(e => e.KarmienieId == id);
        }
    }
}
