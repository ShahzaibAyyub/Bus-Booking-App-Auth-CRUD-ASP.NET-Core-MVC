using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finalDeliverable.Data;
using finalDeliverable.Models;
using Microsoft.AspNetCore.Http;

namespace finalDeliverable.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BookingContext _context;

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookingContext = _context.Booking.Include(b => b.bus).Include(b => b.user);
            return View(await bookingContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.bus)
                .Include(b => b.user)
                .FirstOrDefaultAsync(m => m.bookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["busIdForeignkey"] = new SelectList(_context.Bus, "busID", "busID");
            ViewData["userIdForeignkey"] = new SelectList(_context.User, "name", "name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bookingID,busIdForeignkey,userIdForeignkey,time")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["busIdForeignkey"] = new SelectList(_context.Bus, "busID", "busID", booking.busIdForeignkey);
            ViewData["userIdForeignkey"] = new SelectList(_context.User, "name", "name", booking.userIdForeignkey);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["busIdForeignkey"] = new SelectList(_context.Bus, "busID", "busID", booking.busIdForeignkey);
            ViewData["userIdForeignkey"] = new SelectList(_context.User, "name", "name", booking.userIdForeignkey);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("bookingID,busIdForeignkey,userIdForeignkey,time")] Booking booking)
        {
            if (id != booking.bookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.bookingID))
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
            ViewData["busIdForeignkey"] = new SelectList(_context.Bus, "busID", "busID", booking.busIdForeignkey);
            ViewData["userIdForeignkey"] = new SelectList(_context.User, "name", "name", booking.userIdForeignkey);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.bus)
                .Include(b => b.user)
                .FirstOrDefaultAsync(m => m.bookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(string id)
        {
            return _context.Booking.Any(e => e.bookingID == id);
        }

       
        public async Task<IActionResult> makeEnrollement2(Bus bus)
        {
            string username = HttpContext.Session.GetString("Name");
            var customer = await _context.User.FindAsync(username);
            ;
            string busInfo = bus.busID;
            DateTime t = bus.time;
            DateTime t2 = DateTime.Now;
            int timeCheck = DateTime.Compare(t, t2);
            int h = 0;
            if (timeCheck > 0 && bus.Capacity > 0)
            {
                Booking booking = new Booking();
                booking.busIdForeignkey = bus.busID;
                booking.userIdForeignkey = customer.name;
                bool check = true;
                string temp = "";
                while (check == true)
                {
                    Random rnd = new Random();
                    int num = rnd.Next(1000);
                    temp = num.ToString();
                    check = BookingExists(temp);
                }

                booking.bookingID = temp;
                booking.time = t2;
                ///
                bus.counter++;
                bus.Capacity--;
                _context.Update(bus);
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Bookings");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Bookings");
        }

        public async Task<IActionResult> customerRecord(string username)
        {

            username = HttpContext.Session.GetString("Name");
            IEnumerable<Booking> filteringQuery =
            from records in _context.Booking
            where (records.userIdForeignkey==username)
            select records;

            int g = filteringQuery.Count();
        //    Booking b = filteringQuery.ElementAt(0);
        //    string a = b.userIdForeignkey;
           // var customer = await _context.User.FindAsync(username);
            ICollection<Customer> record = new List<Customer>();
            


            if(filteringQuery.Count()==0)
            {
                Booking b = new Booking();
                b.bookingID = "-";
                b.busIdForeignkey = "-";
                b.userIdForeignkey = "-";
                filteringQuery.Append(b);
            }



            ViewBag.data = filteringQuery;
            return View(filteringQuery);
        }






    }
}
