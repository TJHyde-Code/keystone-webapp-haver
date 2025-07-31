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
using Microsoft.AspNetCore.Authorization;

namespace HaverGroupProject.Controllers
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly HaverContext _context;

        public VendorController(HaverContext context)
        {
            _context = context;
        }

        //Index includes Archive
        public async Task<IActionResult> Index(bool showArchived = false)
        {
            ViewData["ShowArchived"] = showArchived;

            //var query = _context.Vendors
            //    .Include(v => v.OperationsSchedules).IgnoreQueryFilters();

            var query = _context.Vendors
                        .Include(v => v.OperationsScheduleVendors)
                         .ThenInclude(osv => osv.OperationsSchedule)
                        .IgnoreQueryFilters();

            if (showArchived)
            {
                query = query.Where(v => v.VendorArchived);
            }
            else
            {
                query = query.Where(v => !v.VendorArchived);
            }

            var vendors = await query.ToListAsync();
            return View(vendors);
        }

        /* Commented out below to avoid duplicate error, once it works I can safely delete
        // GET: Vendor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vendors.ToListAsync());
        }
        */

        // GET: Vendor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                        .Include(v => v.OperationsScheduleVendors)
                        .ThenInclude(osv => osv.OperationsSchedule)
                        .FirstOrDefaultAsync(m => m.ID == id);

            if (vendor == null)
            {
                return NotFound();
            }

            var activeOrders = vendor.OperationsScheduleVendors?
                            .Select(osv => osv.OperationsSchedule)
                            .Where(os => os != null && os.KickoffMeeting.HasValue)
                            .ToList() ?? new List<OperationsSchedule>();

            var viewModel = new VendorDetailsViewModel
            {
                Vendors = vendor,
                ActiveOrdersCount = activeOrders.Count,
                ActiveOrders = activeOrders
            };

            return View(viewModel);
        }

        // GET: Vendor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VendorName")] Vendor vendor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                    return View(vendor); // Return to the view to show validation messages.
                }
                if (ModelState.IsValid)
                {
                    _context.Add(vendor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return View(vendor);
        }

        // GET: Vendor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VendorName")] Vendor vendor)
        {
            if (id != vendor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.ID))
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
            return View(vendor);
        }

        // GET: Vendor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.ID == id);
        }

        // GET: Vendor/Archive/5
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendor/Archive
        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            vendor.VendorArchived = true;
            _context.Update(vendor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Vendor archived successfully!";
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

            var vendor = await _context.Vendors
                .IgnoreQueryFilters()
                .Where(v => v.ID == id && v.VendorArchived)
                .FirstOrDefaultAsync();

            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendor/UnArchive
        [HttpPost, ActionName("UnArchive")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnArchiveConfirmed(int id)
        {
            var vendor = await _context.Vendors
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(v => v.ID == id && v.VendorArchived);

            if (vendor == null)
            {
                return NotFound();
            }

            vendor.VendorArchived = false;
            _context.Update(vendor);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Vendor has been unarchived successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
