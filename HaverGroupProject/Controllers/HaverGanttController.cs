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
    public class HaverGanttController : Controller
    {
        private readonly HaverContext _context;

        public HaverGanttController(HaverContext context)
        {
            _context = context;
        }

        // GET: HaverGantt
        public async Task<IActionResult> Index()
        {
            var haverContext = _context.HaverGantts
                .Include(h => h.Customer)
                .Include(h => h.Engineer)
                .Include(h => h.MachineDescription)
                .Include(h => h.Vendor);
            return View(await haverContext.ToListAsync());
        }

        // GET: HaverGantt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haverGantt = await _context.HaverGantts
                .Include(h => h.Customer)
                .Include(h => h.Engineer)
                .Include(h => h.MachineDescription)
                .Include(h => h.Vendor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (haverGantt == null)
            {
                return NotFound();
            }

            return View(haverGantt);
        }

        // GET: HaverGantt/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName");
            ViewData["EngineerID"] = new SelectList(_context.Engineers, "ID", "EngEmail");
            ViewData["MachineDescriptionID"] = new SelectList(_context.MachineDescriptions, "ID", "Class");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "ID", "VendorName");

            PopulateDropDownLists();
            return View();
        }

        // POST: HaverGantt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PurchaseOrderNum,StartDate,PromiseDate,Quantity,ApprvDwgRecvd,Progress,GanttNotes,MachineDescriptionID,CustomerID,EngineerID,VendorId")] HaverGantt haverGantt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(haverGantt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName", haverGantt.CustomerID);
            ViewData["EngineerID"] = new SelectList(_context.Engineers, "ID", "EngEmail", haverGantt.EngineerID);
            ViewData["MachineDescriptionID"] = new SelectList(_context.MachineDescriptions, "ID", "Class", haverGantt.MachineDescriptionID);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "ID", "VendorName", haverGantt.VendorID);

            PopulateDropDownLists(haverGantt);
            return View(haverGantt);
        }

        // GET: HaverGantt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haverGantt = await _context.HaverGantts.FindAsync(id);
            if (haverGantt == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName", haverGantt.CustomerID);
            ViewData["EngineerID"] = new SelectList(_context.Engineers, "ID", "EngEmail", haverGantt.EngineerID);
            ViewData["MachineDescriptionID"] = new SelectList(_context.MachineDescriptions, "ID", "Class", haverGantt.MachineDescriptionID);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "ID", "VendorName", haverGantt.VendorID);
            return View(haverGantt);
        }

        // POST: HaverGantt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PurchaseOrderNum,StartDate,PromiseDate,Quantity,ApprvDwgRecvd,Progress,GanttNotes,MachineDescriptionID,CustomerID,EngineerID,VendorId")] HaverGantt haverGantt)
        {
            if (id != haverGantt.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haverGantt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaverGanttExists(haverGantt.ID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName", haverGantt.CustomerID);
            ViewData["EngineerID"] = new SelectList(_context.Engineers, "ID", "EngEmail", haverGantt.EngineerID);
            ViewData["MachineDescriptionID"] = new SelectList(_context.MachineDescriptions, "ID", "Class", haverGantt.MachineDescriptionID);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "ID", "VendorName", haverGantt.VendorID);

            PopulateDropDownLists(haverGantt);

            return View(haverGantt);
        }

        // GET: HaverGantt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haverGantt = await _context.HaverGantts
                .Include(h => h.Customer)
                .Include(h => h.Engineer)
                .Include(h => h.MachineDescription)
                .Include(h => h.Vendor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (haverGantt == null)
            {
                return NotFound();
            }

            return View(haverGantt);
        }

        // POST: HaverGantt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var haverGantt = await _context.HaverGantts.FindAsync(id);

            try
            {
                if (haverGantt != null)
                {
                    _context.HaverGantts.Remove(haverGantt);
                }
            }
            catch (DbUpdateException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator");
            }
           

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Retrieves all Gantt tasks from the database and formats them for the frontend.
        /// Converts task properties to the expected format for the Gantt chart.
        /// Progress is multiplied by 100 to convert from decimal (0.0-1.0) to percentage (0-100%).
        /// *IMPORTANTANT* Property names should be in lowercase for consistency with JavaScript expectations.
        /// </summary>
        /// <returns>JSON response with formatted tasks</returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.HaverGantts.ToListAsync();
            var formattedTasks = tasks.Select(t => new
            {
                id = t.ID.ToString(),
                customer = t.Customer,
                start = t.StartDate.ToString("yyyy-MM-dd"),
                end = t.PromiseDate.ToString("yyyy-MM-dd"),
                progress = t.Progress * 100
            }).ToList();

            return Json(formattedTasks);
        }


        /// <summary>
        /// Updates an existing Gantt task with new values from the request body.
        /// Finds the task by ID, updates its start date, end date, and progress.
        /// Progress should be passed as a decimal (0.0 - 1.0).
        /// Returns a JSON response indicating success or failure.
        /// </summary>
        /// <param name="updatedTask">The task object containing updated values.</param>
        /// <returns>JSON response with success status and message.</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateTask([FromBody] HaverGantt updatedTask)
        {
            var existingTask = await _context.HaverGantts.FindAsync(updatedTask.ID);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.StartDate = updatedTask.StartDate;
            existingTask.PromiseDate = updatedTask.PromiseDate;
            existingTask.Progress = updatedTask.Progress;

            _context.HaverGantts.Update(existingTask);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Task updated successfully." });
        }


        private SelectList MachineDescList(int? selectedId)
        {
            return new SelectList(_context.MachineDescriptions
                .OrderBy(d => d.Class), "ID", "DescriptionSummary", selectedId);                
        }

        private SelectList EngineerList(int? selectedId)
        {
            return new SelectList(_context.Engineers
                .OrderBy(d => d.EngLastName)
                .ThenBy(d=> d.EngFirstName), "ID", "EngSummary", selectedId);
        }

        private void PopulateDropDownLists(HaverGantt? haverGantt = null)
        {
            ViewData["EngineerID"]= EngineerList(haverGantt?.EngineerID);
            ViewData["MachineDescriptionID"] = MachineDescList(haverGantt?.MachineDescriptionID)
            ;
        }
        private bool HaverGanttExists(int id)
        {
            return _context.HaverGantts.Any(e => e.ID == id);
        }
    }
}
