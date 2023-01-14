using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineReservations.Areas.Identity.Data;
using AirlineReservations.Models;
using Microsoft.AspNetCore.Authorization;

namespace AirlineReservations.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BookingClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index()
        {
              return View(await _context.bookingClass.ToListAsync());
        }

     
        public IActionResult Create()
        {
            return View();
        }

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
