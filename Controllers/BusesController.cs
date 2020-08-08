using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finalDeliverable.Data;
using finalDeliverable.Models;

namespace finalDeliverable.Controllers
{
    public class BusesController : Controller
    {
        private readonly BookingContext _context;

        public BusesController(BookingContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bus.ToListAsync());
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .FirstOrDefaultAsync(m => m.busID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("busID,From,To,Capacity,counter,price,time")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("busID,From,To,Capacity,counter,price,time")] Bus bus)
        {
            if (id != bus.busID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.busID))
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
            return View(bus);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .FirstOrDefaultAsync(m => m.busID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bus = await _context.Bus.FindAsync(id);
            _context.Bus.Remove(bus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(string id)
        {
            return _context.Bus.Any(e => e.busID == id);
        }


        public async Task<IActionResult> Details2(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .FirstOrDefaultAsync(m => m.busID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return RedirectToAction("makeEnrollement2", "Bookings", bus);
        }



        public async Task<IActionResult> seed()
        {
            Bus b1 = new Bus()
            {
                busID = "xyz-1",
                Capacity = 30,
                counter = 0,
                From = "islamabad",
                To="karachi",
                price=1000,
                time= System.DateTime.Now


            };
            bool check1 = BusExists(b1.busID);
            if(check1==false)
            {
                _context.Add(b1);
                await _context.SaveChangesAsync();

            }




            Bus b2 = new Bus()
            {
                busID = "xyz-2",
                Capacity = 30,
                counter = 0,
                From = "islamabad",
                To = "lahore",
                price = 1000,
                time = System.DateTime.Now


            };
            bool check2 = BusExists(b2.busID);
            if (check2 == false)
            {
                _context.Add(b2);
                await _context.SaveChangesAsync();

            }





            return RedirectToAction("Index", "Customers");



           
        }





    }
}
