using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HaverGroupProject.Data;
using HaverGroupProject.Models;
using HaverGroupProject.ViewModels;

namespace HaverGroupProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HaverContext _context;

        public CustomerController(HaverContext context)
        {
            _context = context;
        }

        //Index includes Archive
        public async Task<IActionResult> Index(bool showArchived = false)
        {
            ViewData["ShowArchived"] = showArchived;

            // Start building the query for customers
            var query = _context.Customers
                .Include(c => c.OperationsSchedule)                      
                .IgnoreQueryFilters();             

            // Apply filtering for archived customers based on the 'showArchived' parameter
            if (showArchived)
            {
                query = query.Where(c => c.CustomerArchived);
            }
            else
            {
                query = query.Where(c => !c.CustomerArchived);
            }

            // Fetch customers along with their KickOffMeetings and Orders
            var customers = await query.ToListAsync();           

            return View(customers);
        }

        /* commented out old index for now
        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers
                .Include(o => o.OperationsSchedule)
                .ToListAsync());
        }
        */

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c =>c.OperationsSchedule)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            var activeOrders = customer.OperationsSchedule
                .Where(os => os.KickoffMeeting.HasValue)  // Check if KickoffMeeting is set (active order)
            .ToList();

            // Prepare data to pass to the view
            var viewModel = new CustomerDetailsViewModel
            {
                Customer = customer,
                ActiveOrdersCount = activeOrders.Count,
                ActiveOrders = activeOrders
            };

            return View(viewModel);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CustomerName,ReleaseDate")] Customer customer, string CustomerName)
        {
            if (string.IsNullOrEmpty(CustomerName))
            {
                return Json(new { success = false, message = "Customer Name is required." });
            }

            var newCustomer = new Customer { CustomerName = CustomerName };

            _context.Add(newCustomer);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerName,ReleaseDate")] Customer customer)
        {
            if (id != customer.ID)
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
                    if (!CustomerExists(customer.ID))
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

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }

        // GET: Customer/Archive/5
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Archive
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerArchived = true;
            _context.Update(customer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Customer archived successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Vendor/UnArchive/5
        [HttpGet]
        public async Task<IActionResult> UnArchive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .IgnoreQueryFilters()
                .Where(c => c.ID == id && c.CustomerArchived)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/UnArchive
        [HttpPost, ActionName("UnArchive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnArchiveConfirmed(int id)
        {
            var customer = await _context.Customers
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.ID == id && c.CustomerArchived);

            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerArchived = false;
            _context.Update(customer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Customer has been unarchived successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
