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
    public class CustomersController : Controller
    {
        private readonly BookingContext _context;
        const string SessionName = "Name";

        public CustomersController(BookingContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.User
                .FirstOrDefaultAsync(m => m.name == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,passward,gender,age")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.User.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("name,passward,gender,age")] Customer customer)
        {
            if (id != customer.name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.name))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.User
                .FirstOrDefaultAsync(m => m.name == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.User.FindAsync(id);
            _context.User.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.User.Any(e => e.name == id);
        }




        private bool CustomerExists2(string id)
        {
            return _context.User.Any(e => e.passward == id);
        }



        public IActionResult loginTrigger()
        {
            return View();
        }

        public IActionResult trackRecord()
        {
            string username = HttpContext.Session.GetString("Name");
            return RedirectToAction("customerRecord","Bookings",username);
        }




        [HttpGet]
        public IActionResult LoginFetcher(Customer user)
        {

            bool check1 = CustomerExists2(user.passward);
            bool check2 = CustomerExists(user.name);
            if (check1 == true && check2 == true)
            {

                HttpContext.Session.SetString(SessionName, user.name);
                //user.BookingHistory = new List<Booking>();
                return View();
                //return  RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Home", "Contact");
        }


        


    }
}
