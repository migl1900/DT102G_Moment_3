using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT102G_Moment3.Data;
using DT102G_Moment3.Models;

namespace DT102G_Moment3.Controllers
{
    public class LendingController : Controller
    {
        private readonly CollectionContext _context;

        public LendingController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Lending
        [HttpGet("/Utlaning")]
        public async Task<IActionResult> Index()
        {
            var collectionContext = _context.Lendings.Include(l => l.Record);
            return View(await collectionContext.ToListAsync());
        }

        // GET: Lending/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lendings
                .Include(l => l.Record)
                .FirstOrDefaultAsync(m => m.LendingId == id);
            if (lending == null)
            {
                return NotFound();
            }

            return View(lending);
        }

        // GET: Lending/Create
        public IActionResult Create()
        {
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Name");
            return View();
        }

        // POST: Lending/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LendingId,Name,Date,RecordId")] Lending lending)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lending);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Name", lending.RecordId);
            return View(lending);
        }

        // GET: Lending/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lendings.FindAsync(id);
            if (lending == null)
            {
                return NotFound();
            }
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Name", lending.RecordId);
            return View(lending);
        }

        // POST: Lending/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LendingId,Name,Date,RecordId")] Lending lending)
        {
            if (id != lending.LendingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lending);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendingExists(lending.LendingId))
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
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Name", lending.RecordId);
            return View(lending);
        }

        // GET: Lending/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lending = await _context.Lendings
                .Include(l => l.Record)
                .FirstOrDefaultAsync(m => m.LendingId == id);
            if (lending == null)
            {
                return NotFound();
            }

            return View(lending);
        }

        // POST: Lending/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lending = await _context.Lendings.FindAsync(id);
            _context.Lendings.Remove(lending);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LendingExists(int id)
        {
            return _context.Lendings.Any(e => e.LendingId == id);
        }
    }
}
