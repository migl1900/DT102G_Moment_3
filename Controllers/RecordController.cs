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
    public class RecordController : Controller
    {
        private readonly CollectionContext _context;

        public RecordController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Record
        public async Task<IActionResult> Index(string searchString) // Enable filtering result
        {
            var lendings = _context.Lendings.ToList(); // Get all posts in Lending table
            ViewData["Lendings"] = lendings; // Send list to view
            var collectionContext = _context.Records.Include(item => item.Artist); // Get all posts from Records and Artists

            // If a search is made get specific data
            if (!String.IsNullOrEmpty(searchString))
            {
                collectionContext = _context.Records.Where(s => s.Name.Contains(searchString)).Include(item => item.Artist);
            }
            return View(await collectionContext.ToListAsync());
        }

        // GET: Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(item => item.Artist)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // GET: Record/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name");
            return View();
        }

        // POST: Record/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,Name,Year,ArtistId")] Record @record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", @record.ArtistId);
            return View(@record);
        }

        // GET: Record/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records.FindAsync(id);
            if (@record == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "Name", @record.ArtistId);
            return View(@record);
        }

        // POST: Record/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,Name,Year,ArtistId")] Record @record)
        {
            if (id != @record.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(@record.RecordId))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", @record.ArtistId);
            return View(@record);
        }

        // GET: Record/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(item => item.Artist)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @record = await _context.Records.FindAsync(id);
            _context.Records.Remove(@record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.RecordId == id);
        }
    }
}
