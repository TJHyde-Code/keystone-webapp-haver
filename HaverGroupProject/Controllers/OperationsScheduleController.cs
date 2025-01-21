using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HaverGroupProject.Data;
using HaverGroupProject.Models;

namespace HaverGroupProject.Controllers
{
    public class OperationsScheduleController : Controller
    {
        private readonly HaverContext _context;

        public OperationsScheduleController(HaverContext context)
        {
            _context = context;
        }

        // GET: OperationsSchedule
        public async Task<IActionResult> Index()
        {
            var haverContext = _context.OperationsSchedules.Include(o => o.Customer).Include(o => o.Vendor);
            return View(await haverContext.ToListAsync());
        }

        // GET: OperationsSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (operationsSchedule == null)
            {
                return NotFound();
            }

            return View(operationsSchedule);
        }

        // GET: OperationsSchedule/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID");
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID");
            return View();
        }

        // POST: OperationsSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SalesOrdNum,ExtSalesOrdNum,CustomerID,MachineDesc,SerialNum,PackageReleaseName,KickoffMeeting,ReleaseApprovalDrawing,VendorID,PONum,PODueDate,DeliveryDate,InstalledMedia,SparePartsSpareMedia,BaseFrame,AirSeal,CoatingLining,Disassebmly")] OperationsSchedule operationsSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationsSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", operationsSchedule.CustomerID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", operationsSchedule.VendorID);
            return View(operationsSchedule);
        }

        // GET: OperationsSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationsSchedule = await _context.OperationsSchedules.FindAsync(id);
            if (operationsSchedule == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", operationsSchedule.CustomerID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", operationsSchedule.VendorID);
            return View(operationsSchedule);
        }

        // POST: OperationsSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SalesOrdNum,ExtSalesOrdNum,CustomerID,MachineDesc,SerialNum,PackageReleaseName,KickoffMeeting,ReleaseApprovalDrawing,VendorID,PONum,PODueDate,DeliveryDate,InstalledMedia,SparePartsSpareMedia,BaseFrame,AirSeal,CoatingLining,Disassebmly")] OperationsSchedule operationsSchedule)
        {
            if (id != operationsSchedule.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operationsSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationsScheduleExists(operationsSchedule.ID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", operationsSchedule.CustomerID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", operationsSchedule.VendorID);
            return View(operationsSchedule);
        }

        // GET: OperationsSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (operationsSchedule == null)
            {
                return NotFound();
            }

            return View(operationsSchedule);
        }

        // POST: OperationsSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operationsSchedule = await _context.OperationsSchedules.FindAsync(id);
            if (operationsSchedule != null)
            {
                _context.OperationsSchedules.Remove(operationsSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationsScheduleExists(int id)
        {
            return _context.OperationsSchedules.Any(e => e.ID == id);
        }
    }
}
