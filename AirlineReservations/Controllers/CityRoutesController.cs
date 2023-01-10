﻿using System;
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
    public class CityRoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityRoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CityRoutes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.routes.Include(c => c.Airline);
            return View(await applicationDbContext.ToListAsync());
        }

      
        public IActionResult Create()
        {
            ViewData["AirlineId"] = new SelectList(_context.airlines, "Id", "AirlineName");
            return View();
        }

        // POST: CityRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,RouteFromCity,RouteToCity,AirlineId")] CityRoute cityRoute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cityRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineId"] = new SelectList(_context.airlines, "Id", "AirlineName", cityRoute.AirlineId);
            return View(cityRoute);
        }

        // GET: CityRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.routes == null)
            {
                return NotFound();
            }

            var cityRoute = await _context.routes.FindAsync(id);
            if (cityRoute == null)
            {
                return NotFound();
            }
            ViewData["AirlineId"] = new SelectList(_context.airlines, "Id", "AirlineName", cityRoute.AirlineId);
            return View(cityRoute);
        }

        // POST: CityRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,RouteFromCity,RouteToCity,AirlineId")] CityRoute cityRoute)
        {
            if (id != cityRoute.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cityRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityRouteExists(cityRoute.RouteId))
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
            ViewData["AirlineId"] = new SelectList(_context.airlines, "Id", "AirlineName", cityRoute.AirlineId);
            return View(cityRoute);
        }

        // GET: CityRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.routes == null)
            {
                return NotFound();
            }

            var cityRoute = await _context.routes
                .Include(c => c.Airline)
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (cityRoute == null)
            {
                return NotFound();
            }

            return View(cityRoute);
        }

        // POST: CityRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.routes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.routes'  is null.");
            }
            var cityRoute = await _context.routes.FindAsync(id);
            if (cityRoute != null)
            {
                _context.routes.Remove(cityRoute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityRouteExists(int id)
        {
          return _context.routes.Any(e => e.RouteId == id);
        }
    }
}
