using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineReservations.Areas.Identity.Data;
using AirlineReservations.Models;

namespace AirlineReservations.Controllers
{
    public class BookingClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingClasses
        public async Task<IActionResult> Index()
        {
              return View(await _context.bookingClass.ToListAsync());
        }

        // GET: BookingClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.bookingClass == null)
            {
                return NotFound();
            }

            var bookingClass = await _context.bookingClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingClass == null)
            {
                return NotFound();
            }

            return View(bookingClass);
        }

        // GET: BookingClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassTpye")] BookingClass bookingClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookingClass);
        }

        // GET: BookingClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.bookingClass == null)
            {
                return NotFound();
            }

            var bookingClass = await _context.bookingClass.FindAsync(id);
            if (bookingClass == null)
            {
                return NotFound();
            }
            return View(bookingClass);
        }

        // POST: BookingClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassTpye")] BookingClass bookingClass)
        {
            if (id != bookingClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingClassExists(bookingClass.Id))
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
            return View(bookingClass);
        }

        // GET: BookingClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.bookingClass == null)
            {
                return NotFound();
            }

            var bookingClass = await _context.bookingClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingClass == null)
            {
                return NotFound();
            }

            return View(bookingClass);
        }

        // POST: BookingClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.bookingClass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.bookingClass'  is null.");
            }
            var bookingClass = await _context.bookingClass.FindAsync(id);
            if (bookingClass != null)
            {
                _context.bookingClass.Remove(bookingClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingClassExists(int id)
        {
          return _context.bookingClass.Any(e => e.Id == id);
        }
    }
}
