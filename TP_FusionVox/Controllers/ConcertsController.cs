using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models;
using TP_FusionVox.Models.Data;

namespace TP_FusionVox
{
    public class ConcertsController : Controller
    {
        private readonly TP_FusionVoxDbContext _context;

        public ConcertsController(TP_FusionVoxDbContext context)
        {
            _context = context;
        }

        // GET: Concerts
        [Route("Concerts/Index")]
        public async Task<IActionResult> Index()
        {
              return _context.Concert != null ? 
                          View(await _context.Concert.ToListAsync()) :
                          Problem("Entity set 'TP_FusionVoxDbContext.Concert'  is null.");
        }

        // GET: Concerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Concert == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: Concerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,ImageUrl,DateConcert,Pays,Ville,Scene,NbTotalBillets,NbBilletsVendu,PrixBillet")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Concert == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,ImageUrl,DateConcert,Pays,Ville,Scene,NbTotalBillets,PrixBillet")] Concert concert)
        {
            if (id != concert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concert.Id))
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
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Concert == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Concert == null)
            {
                return Problem("Entity set 'TP_FusionVoxDbContext.Concert'  is null.");
            }
            var concert = await _context.Concert.FindAsync(id);
            if (concert != null)
            {
                _context.Concert.Remove(concert);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(int id)
        {
          return (_context.Concert?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
