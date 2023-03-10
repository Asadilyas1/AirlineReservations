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
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Include(t => t.Airline).Include(t => t.bookingClass);
            return View(await applicationDbContext.ToListAsync());
        }

      
        public IActionResult Create()
        {
            ViewData["AirlineID"] = new SelectList(_context.airlines, "Id", "AirlineName");
            ViewData["ClassTypeID"] = new SelectList(_context.bookingClass, "Id", "ClassTpye");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketFrom,TicletTo,TimeFrom,TimeTo,TotalSeata,SinglePrice,TwoWaysPrice,AirlineID,ClassTypeID")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineID"] = new SelectList(_context.airlines, "Id", "AirlineName", tickets.AirlineID);
            ViewData["ClassTypeID"] = new SelectList(_context.bookingClass, "Id", "ClassTpye", tickets.ClassTypeID);
            return View(tickets);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            ViewData["AirlineID"] = new SelectList(_context.airlines, "Id", "AirlineName", tickets.AirlineID);
            ViewData["ClassTypeID"] = new SelectList(_context.bookingClass, "Id", "ClassTpye", tickets.ClassTypeID);
            return View(tickets);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketFrom,TicletTo,TimeFrom,TimeTo,TotalSeata,SinglePrice,TwoWaysPrice,AirlineID,ClassTypeID")] Tickets tickets)
        {
            if (id != tickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.Id))
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
            ViewData["AirlineID"] = new SelectList(_context.airlines, "Id", "AirlineName", tickets.AirlineID);
            ViewData["ClassTypeID"] = new SelectList(_context.bookingClass, "Id", "ClassTpye", tickets.ClassTypeID);
            return View(tickets);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.Airline)
                .Include(t => t.bookingClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets != null)
            {
                _context.Tickets.Remove(tickets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
          return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
