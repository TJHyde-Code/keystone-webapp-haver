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
using Microsoft.EntityFrameworkCore.Storage;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<IActionResult> Index(
     string SalesOrderNumber,
     string CustomerName,
     string EngineerFirstName,
     string MachineSerialNumber,
     string Vendor,
     string ProductionOrderNumber)
        {
            // First get all necessary data from database
            var operationsSchedules = await _context.OperationsSchedules
                .OrderByDescending(o => o.ID)
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .Include(o => o.Engineer)
                .Include(o => o.OperationsScheduleMachines).ThenInclude(o => o.MachineDescription)
                .Include(o => o.Note)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
                .AsNoTracking()
                .ToListAsync();

            // Apply filters in memory
            IEnumerable<OperationsSchedule> filteredResults = operationsSchedules;

            if (!string.IsNullOrEmpty(SalesOrderNumber))
            {
                filteredResults = filteredResults.Where(o =>
                    o.SalesOrdNum.ToString().Contains(SalesOrderNumber, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(CustomerName))
            {
                filteredResults = filteredResults.Where(o =>
                    o.Customer != null &&
                    o.Customer.CustomerName != null &&
                    o.Customer.CustomerName.Contains(CustomerName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(EngineerFirstName))
            {
                filteredResults = filteredResults.Where(o =>
                    o.Engineer != null &&
                    ((o.Engineer.EngFirstName != null &&
                      o.Engineer.EngFirstName.Contains(EngineerFirstName, StringComparison.OrdinalIgnoreCase)) ||
                     (o.Engineer.EngSummary != null &&
                      o.Engineer.EngSummary.Contains(EngineerFirstName, StringComparison.OrdinalIgnoreCase))));
            }

            if (!string.IsNullOrEmpty(MachineSerialNumber))
            {
                filteredResults = filteredResults.Where(o =>
                    o.MachineDescription != null &&
                    o.MachineDescription.SerialNumber != null &&
                    o.MachineDescription.SerialNumber.Contains(MachineSerialNumber, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(Vendor))
            {
                filteredResults = filteredResults.Where(o =>
                    o.OperationsScheduleVendors != null &&
                    o.OperationsScheduleVendors.Any(v =>
                        v.Vendor != null &&
                        v.Vendor.VendorName != null &&
                        v.Vendor.VendorName.Contains(Vendor, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(ProductionOrderNumber))
            {
                filteredResults = filteredResults.Where(o =>
                    o.ProductionOrderNumber != null &&
                    o.ProductionOrderNumber.Contains(ProductionOrderNumber, StringComparison.OrdinalIgnoreCase));
            }

            // Store filter values
            ViewBag.Filter = new
            {
                SalesOrderNumber,
                CustomerName,
                EngineerFirstName,
                MachineSerialNumber,
                Vendor,
                ProductionOrderNumber
            };

            return View(filteredResults.ToList());
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
                .Include(o => o.Engineer)
                .Include(o => o.OperationsScheduleMachines).ThenInclude(o => o.MachineDescription)
                .Include(o => o.Note)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName");
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID");

            OperationsSchedule operationsSchedule = new OperationsSchedule();
            PopulateAssignedVendorData(operationsSchedule);

            return View(operationsSchedule);
        }

        // POST: OperationsSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SalesOrdNum,ExtSalesOrdNum,CustomerID,MachineDesc,SerialNum,PackageReleaseName,KickoffMeeting,ReleaseApprovalDrawing,VendorID,PONum,PODueDate,DeliveryDate,InstalledMedia,SparePartsSpareMedia,BaseFrame,AirSeal,CoatingLining,Disassebmly")] OperationsSchedule operationsSchedule, string[] selectedOptions)
        {
            try
            {
                UpdateOperationsScheduleVendors(selectedOptions, operationsSchedule);

                if (ModelState.IsValid)
                {
                    _context.Add(operationsSchedule);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts.");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }

            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName", operationsSchedule.CustomerID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", operationsSchedule.VendorID);

            PopulateAssignedVendorData(operationsSchedule);
            return View(operationsSchedule);
        }

        // GET: OperationsSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.Engineer)
                .Include(o => o.MachineDescription)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(v => v.Vendor)
                .Include(o => o.Note)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
                SalesOrdNum = operationsSchedule.SalesOrdNum,
                //ExtSalesOrdNum = operationsSchedule.ExtSalesOrdNum,
                //PackageReleaseName = operationsSchedule.PackageReleaseName,
                KickoffMeeting = operationsSchedule.KickoffMeeting,
                //ReleaseApprovalDrawing = operationsSchedule.ReleaseApprovalDrawing,

                CustomerID = operationsSchedule.CustomerID,
                Customers = _context.Customers
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.CustomerName })
                    .ToList(),

                MachineDescriptionID = operationsSchedule.MachineDescriptionID,
                Machines = _context.MachineDescriptions
                    .Select(m => new SelectListItem { Value = m.ID.ToString(), Text = $"{m.SerialNumber} - {m.Size} - {m.Class} - {m.Deck}" })
                    .ToList(),

                SelectedVendorIDs = operationsSchedule.OperationsScheduleVendors
                    .Select(ov => ov.VendorID.ToString()).ToArray(),
                Vendors = _context.Vendors
                    .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.VendorName })
                    .ToList(),

                ProductionOrderNumber = operationsSchedule.ProductionOrderNumber,
                //PODueDate = operationsSchedule.PODueDate,
                //DeliveryDate = operationsSchedule.DeliveryDate,

                PreOrder = operationsSchedule.Note?.PreOrder,
                Scope = operationsSchedule.Note?.Scope,
                BudgetAssembHrs = operationsSchedule.Note?.OtherComments
            };

            return View(viewModel);
        }

        // POST: OperationsSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MultiStepOperationsScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules
                    .Include(o => o.OperationsScheduleVendors)
                    .FirstOrDefaultAsync(o => o.ID == model.ID);

                if (operationsSchedule == null) return NotFound();

                operationsSchedule.SalesOrdNum = model.SalesOrdNum;
                //operationsSchedule.ExtSalesOrdNum = model.ExtSalesOrdNum;
                //operationsSchedule.PackageReleaseName = model.PackageReleaseName;
                operationsSchedule.KickoffMeeting = model.KickoffMeeting;
                //operationsSchedule.ReleaseApprovalDrawing = model.ReleaseApprovalDrawing;
                operationsSchedule.CustomerID = model.CustomerID;
                operationsSchedule.MachineDescriptionID = model.MachineDescriptionID;
                operationsSchedule.ProductionOrderNumber = model.ProductionOrderNumber;
                //operationsSchedule.PODueDate = model.PODueDate;
                //operationsSchedule.DeliveryDate = model.DeliveryDate;

                if (operationsSchedule.Note == null)
                {
                    operationsSchedule.Note = new Note
                    {
                        PreOrder = model.PreOrder ?? "",
                        Scope = model.Scope ?? "",
                        BudgetAssembHrs = model.BudgetAssembHrs ?? "",
                        ActualAssembHours = model.ActualAssembHours ?? 0,
                        ActualReworkHours = model.ActualReworkHours ?? 0,
                        OtherComments = model.OtherComments ?? ""
                    };
                }
                else
                {
                    operationsSchedule.Note.PreOrder = model.PreOrder ?? "";
                    operationsSchedule.Note.Scope = model.Scope ?? "";
                    operationsSchedule.Note.BudgetAssembHrs = model.BudgetAssembHrs ?? "";
                    operationsSchedule.Note.ActualAssembHours = model.ActualAssembHours ?? 0;
                    operationsSchedule.Note.ActualReworkHours = model.ActualReworkHours ?? 0;
                    operationsSchedule.Note.OtherComments = model.OtherComments ?? "";
                }

                _context.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            model.Customers = _context.Customers
                .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.CustomerName })
                .ToList();

            model.Machines = _context.MachineDescriptions
                .Select(m => new SelectListItem { Value = m.ID.ToString(), Text = $"{m.SerialNumber} - {m.Size} - {m.Class} - {m.Deck}" })
                .ToList();

            model.Vendors = _context.Vendors
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.VendorName })
                .ToList();

            return View(model);
        }

        // GET: OperationsSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
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
            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (operationsSchedule != null)
            {
                _context.OperationsSchedules.Remove(operationsSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //STEP 1
        //This loads the form where the user fills out the OperationsSchedule details
        //GET
        public async Task<IActionResult> Step1(int? id)
        {
            if (id.HasValue)
            {
                var operationsSchedule = await _context.OperationsSchedules.FindAsync(id.Value);
                if (operationsSchedule == null) return NotFound();

                var viewModel = new MultiStepOperationsScheduleViewModel
                {
                    ID = operationsSchedule.ID,
                    SalesOrdNum = operationsSchedule.SalesOrdNum,
                    //ExtSalesOrdNum = operationsSchedule.ExtSalesOrdNum,
                    //PackageReleaseName = operationsSchedule.PackageReleaseName,
                    //KickoffMeeting = operationsSchedule.KickoffMeeting,
                    //ReleaseApprovalDrawing = operationsSchedule.ReleaseApprovalDrawing
                };
                return View(viewModel);
            }
            return View(new MultiStepOperationsScheduleViewModel());
        }

        //This saves the partial data and redirects to Step 2
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step1(MultiStepOperationsScheduleViewModel model, string? save)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules.FindAsync(model.ID);

                if (operationsSchedule == null)
                {
                    operationsSchedule = new OperationsSchedule
                    {
                        SalesOrdNum = model.SalesOrdNum,
                        //ExtSalesOrdNum = model.ExtSalesOrdNum,
                        //PackageReleaseName = model.PackageReleaseName,
                        //KickoffMeeting = model.KickoffMeeting,
                        //ReleaseApprovalDrawing = model.ReleaseApprovalDrawing
                    };
                    _context.OperationsSchedules.Add(operationsSchedule);
                }
                else
                {
                    operationsSchedule.SalesOrdNum = model.SalesOrdNum;
                    //operationsSchedule.ExtSalesOrdNum = model.ExtSalesOrdNum;
                    //operationsSchedule.PackageReleaseName = model.PackageReleaseName;
                    //operationsSchedule.KickoffMeeting = model.KickoffMeeting;
                    //operationsSchedule.ReleaseApprovalDrawing = model.ReleaseApprovalDrawing;

                    _context.OperationsSchedules.Update(operationsSchedule);
                }

                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Step2", new { id = operationsSchedule.ID });
            }
            return View(model);
        }

        //STEP 2
        //This loads the form to select or create a customer
        //GET
        public async Task<IActionResult> Step2(int? id)
        {
            var operationsSchedule = await _context.OperationsSchedules.FindAsync(id);
            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
                CustomerID = operationsSchedule.CustomerID,

                Customers = _context.Customers
                .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.CustomerName })
                .ToList(),

                EngineerID = operationsSchedule.EngineerID,

                Engineers = _context.Engineers
                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.EngSummary })
                .ToList(),
            };
            return View(viewModel);

        }

        //This saves the partial data and redirects to Step 3
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step2(MultiStepOperationsScheduleViewModel model, string? save)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules.FindAsync(model.ID);
                if (operationsSchedule == null) return NotFound();

                operationsSchedule.CustomerID = model.CustomerID;
                operationsSchedule.EngineerID = model.EngineerID;
                operationsSchedule.KickoffMeeting = model.KickoffMeeting;
                operationsSchedule.ApprovalDrawingExpected = model.ApprovalDrawingExpected.Value;
                operationsSchedule.PreOrderExpected = model.PreOrderExpected.Value;
                operationsSchedule.EngineerPackageExpected = model.EngineerPackageExpected.Value;
                operationsSchedule.PurchaseOrderExpected = model.PurchaseOrderExpected.Value;
                operationsSchedule.ReadinessToShipExpected = model.ReadinessToShipExpected.Value;

                _context.OperationsSchedules.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Step3", new { id = operationsSchedule.ID });
            }
            model.Customers = _context.Customers
                .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.CustomerName })
                .ToList();

            model.Engineers = _context.Engineers
                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.EngSummary })
                .ToList();

            return View(model);
        }

        //STEP 3
        //This loads the form to select or create a Machine
        //GET
        public async Task<IActionResult> Step3(int id)
        {
            var operationsSchedule = await _context.OperationsSchedules.FindAsync(id);
            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
                MachineDescriptionID = operationsSchedule.MachineDescriptionID,
                Machines = _context.MachineDescriptions
                    .Select(m => new SelectListItem { Value = m.ID.ToString(), Text = $"{m.SerialNumber} - {m.Size} - {m.Class} - {m.Deck}" })
                    .ToList()
            };
            return View(viewModel);
        }

        //This saves the partial data and redirects to Step 4
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step3(MultiStepOperationsScheduleViewModel model, string? save)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules.FindAsync(model.ID);
                if (operationsSchedule == null) return NotFound();

                operationsSchedule.MachineDescriptionID = model.MachineDescriptionID;

                _context.OperationsSchedules.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Step4", new { id = operationsSchedule.ID });
            }
            //model.Machines = _context.MachineDescriptions
            //    .Select(m => new SelectListItem { Value = m.ID.ToString(), Text = $"{m.SerialNumber} - {m.Size} - {m.Class} - {m.Deck}"})
            //    .ToList();

            return View(model);
        }

        // POST: AddMachineToSchedule
        [HttpPost]
        public async Task<IActionResult> AddMachineToSchedule(int machineDescriptionId, int operationsScheduleId)
        {
            // Find the operations schedule by ID
            var operationsSchedule = await _context.OperationsSchedules.FindAsync(operationsScheduleId);
            if (operationsSchedule == null)
            {
                return NotFound(); // If the operations schedule is not found
            }

            // Find the machine description by ID
            var machineDescription = await _context.MachineDescriptions.FindAsync(machineDescriptionId);
            if (machineDescription == null)
            {
                return NotFound(); // If the machine description is not found
            }

            var newLink = new OperationsScheduleMachine
            {
                OperationsScheduleID = operationsSchedule.ID,
                MachineDescriptionID = machineDescription.ID
            };

            // Save the changes to the database
            _context.OperationsScheduleMachines.Add(newLink);
            await _context.SaveChangesAsync();

            // Optionally, return a success status or redirect to the next step
            return Json(new { success = true });
        }


        //STEP 4
        //This loads the form to select or create a Vendor
        //GET
        public async Task<IActionResult> Step4(int id)
        {
            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.OperationsScheduleVendors)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
                SelectedVendorIDs = operationsSchedule.OperationsScheduleVendors
                    .Select(ov => ov.VendorID.ToString()).ToArray(),

                Vendors = _context.Vendors
                    .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.VendorName })
                    .ToList(),

                ProductionOrderNumber = operationsSchedule.ProductionOrderNumber,
                //PODueDate = operationsSchedule.PODueDate,
                //DeliveryDate = operationsSchedule.DeliveryDate

            };
            return View(viewModel);
        }


        //This saves the partial data and redirects to the index page
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step4(MultiStepOperationsScheduleViewModel model, string? save)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules
                    .Include(o => o.OperationsScheduleVendors)
                    .FirstOrDefaultAsync(o => o.ID == model.ID);

                if (operationsSchedule == null) return NotFound();

                operationsSchedule.OperationsScheduleVendors.Clear();

                if (model.SelectedVendorIDs != null)
                {
                    foreach (var vendorID in model.SelectedVendorIDs)
                    {
                        operationsSchedule.OperationsScheduleVendors.Add(new OperationsScheduleVendor
                        {
                            OperationsScheduleID = operationsSchedule.ID,
                            VendorID = int.Parse(vendorID)
                        });
                    }
                }

                operationsSchedule.ProductionOrderNumber = model.ProductionOrderNumber;
                //operationsSchedule.PODueDate = model.PODueDate;
                //operationsSchedule.DeliveryDate = model.DeliveryDate;

                _context.OperationsSchedules.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Step5", new { id = operationsSchedule.ID });
            }
            model.Vendors = _context.Vendors
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.VendorName })
                .ToList();

            return View(model);
        }

        //STEP 5
        //This loads the form to update machineDescription bool fields and add notes fields
        //GET
        public async Task<IActionResult> Step5(int id)
        {
            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.Note)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
                PreOrder = operationsSchedule.Note?.PreOrder,
                Scope = operationsSchedule.Note?.Scope,
                BudgetAssembHrs = operationsSchedule.Note?.BudgetAssembHrs,
                ActualAssembHours = operationsSchedule.Note?.ActualAssembHours,
                ActualReworkHours = operationsSchedule.Note?.ActualReworkHours,
                OtherComments = operationsSchedule.Note?.OtherComments
            };
            return View(viewModel);
        }

        //This saves the updated MachineDescriptions fields and notes and redirects to index
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step5(MultiStepOperationsScheduleViewModel model, string? save)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules
                    .Include(o => o.Note)
                    .FirstOrDefaultAsync(o => o.ID == model.ID);

                if (operationsSchedule == null) return NotFound();


                if (operationsSchedule.Note == null)
                {
                    operationsSchedule.Note = new Note();
                }

                operationsSchedule.Note.PreOrder = model.PreOrder ?? "";
                operationsSchedule.Note.Scope = model.Scope ?? "";
                operationsSchedule.Note.BudgetAssembHrs = model.BudgetAssembHrs ?? "";
                operationsSchedule.Note.ActualAssembHours = model.ActualAssembHours ?? 0;
                operationsSchedule.Note.ActualReworkHours = model.ActualReworkHours ?? 0;
                operationsSchedule.Note.OtherComments = model.OtherComments ?? "";

                _context.OperationsSchedules.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        //Excel Download
        //
        public IActionResult DownloadOperationsSchedules()
        {
            // Get the appointments
            var schedules = from os in _context.OperationsSchedules
                            .Include(os => os.Customer)
                            .Include(os => os.Vendor)
                            orderby os.KickoffMeeting descending
                            select new
                            {
                                SalesOrder = os.SalesOrdNum,
                                Customer = os.Customer.CustomerName,
                                Date = os.KickoffMeeting.HasValue ? os.KickoffMeeting.Value : (DateTime?)null, // Keep as DateTime
                                Machine = os.MachineDescription.DescriptionSummary,
                                Serial_Number = os.MachineDescription.SerialNumber,
                                Engineer = os.Engineer.EngSummary,
                                Vendor = os.Vendor.VendorName,
                                PO_Num = os.PONum,
                                /*DeliveryDate = os.DeliveryDate.HasValue ? os.DeliveryDate.Value : (DateTime?)null // Keep as DateTime*/
                            };

            // How many rows?
            int numRows = schedules.Count();

            if (numRows > 0) // We have data
            {
                // Create a new spreadsheet from scratch.
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var workSheet = excel.Workbook.Worksheets.Add("OperationsSchedules");

                    // Load data into the worksheet
                    workSheet.Cells[3, 1].LoadFromCollection(schedules, true);

                    // Apply formatting for date columns only
                    FormatDateColumn(workSheet, 3, "Date");  // Column 3 for KickoffMeeting (Date)
                    FormatDateColumn(workSheet, 9, "DeliveryDate");  // Column 9 for DeliveryDate

                    // Style the headings
                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 9])
                    {
                        headings.Style.Font.Bold = true;
                        var fill = headings.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.LightBlue);
                    }

                    workSheet.Cells[1, 1].Value = "Operations Schedules Report";
                    using (ExcelRange titleRange = workSheet.Cells[1, 1, 1, 9])
                    {
                        titleRange.Merge = true;
                        titleRange.Style.Font.Bold = true;
                        titleRange.Style.Font.Size = 18;
                        titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    DateTime utcDate = DateTime.UtcNow;
                    TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                    workSheet.Cells[2, 9].Value = "Created: " + localDate.ToString("yyyy-MM-dd HH:mm");
                    workSheet.Cells[2, 9].Style.Font.Bold = true;
                    workSheet.Cells[2, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    workSheet.Cells.AutoFitColumns();

                    try
                    {
                        Byte[] theData = excel.GetAsByteArray();
                        string filename = "OperationsSchedules.xlsx";
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        return File(theData, mimeType, filename);
                    }
                    catch (Exception)
                    {
                        return BadRequest("Could not build and download the file.");
                    }
                }
            }
            return NotFound("No data available.");
        }


        /// <summary>
        /// Gantt Methods for auto generating the Gantt view. 
        /// </summary>
        /// <returns></returns>
        #region Gantt Methods

        [HttpGet]
        public async Task<IActionResult> Gantt(int id)
        {

            // Fetch data from the database for the OperationsSchedule
            var ganttList = _context.OperationsSchedules
                 .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .Include(o => o.Engineer)
                .Include(o => o.OperationsScheduleMachines).ThenInclude(o => o.MachineDescription)
                .Include(o => o.Note)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor);


            // Return the Gantt view with the data model
            return View(await ganttList.ToListAsync());
        }



        /// <summary>
        /// Retrieves all Gantt tasks from the database and formats them for the frontend.
        /// Converts task properties to the expected format for the Gantt chart.        
        /// *IMPORTANTANT* Property names should be in lowercase for consistency with JavaScript expectations.
        /// </summary>
        /// <returns>JSON response with formatted tasks</returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {

            var tasks = await _context.OperationsSchedules                
                .Include(g => g.Customer)
                .ToListAsync();

            var formattedTasks = tasks.Select(t => new
            {
                id = t.ID,
                customer = t.Customer?.CustomerName,
                
                dateRanges = new List<DateRange>
                {
                    new DateRange
                     {
                     Name = "Approval Drawing ",
                     StartDate = t.ApprovalDrawingExpected,
                     EndDate = t.ApprovalDrawingReleased.GetValueOrDefault(t.ApprovalDrawingExpected.AddHours(5)),
                     Color = "#ff9f89",
                     Progress = t.ProgressApprovalDrawing ?? 0
                     },
                     new DateRange
                     {
                     Name = "PreOrder ",
                     StartDate = t.PreOrderExpected,
                     EndDate = t.PreOrderReleased.GetValueOrDefault(t.PreOrderExpected.AddHours(5)),
                     Color = "#85d1f2",
                     Progress = t.ProgressPreOrder ?? 0
                     },
                    new DateRange{
                     Name = "Eng Pckg",
                     StartDate = t.EngineerPackageExpected,
                     EndDate = t.EngineerPackageReleased.GetValueOrDefault(t.EngineerPackageExpected.AddHours(5)),
                     Color = "#f6ff7c",
                     Progress = t.ProgressEngineerPackage ?? 0
                    },
                    new DateRange{
                     Name = "Purch Ord",
                     StartDate = t.PurchaseOrderExpected,
                     EndDate = t.PurchaseOrderDueDate.GetValueOrDefault(t.PurchaseOrderExpected.AddHours(5)),
                     Color = "#90e39d",
                     Progress = t.ProgressPurchaseOrder ?? 0
                    },
                    new DateRange{ 
                     Name = "RTS",
                     StartDate = t.ReadinessToShipExpected,
                     EndDate = t.ReadinessToShipActual.GetValueOrDefault(t.ReadinessToShipExpected.AddHours(5)),
                     Color = "#f3c8f1",
                     Progress = t.ProgressReadinesstoShip ?? 0
                    }
                 }
            }).ToList();

            return Json(formattedTasks);
        }

        //GET: Gantt Update        
        public async Task<IActionResult> GanttUpdate(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }

            var gantt = await _context.OperationsSchedules
            .OrderByDescending(o => o.ID)
            .Include(o => o.Customer)
            .Include(o => o.Vendor)
            .Include(o => o.Engineer)
            .Include(o => o.OperationsScheduleMachines).ThenInclude(o => o.MachineDescription)
            .Include(o => o.Note)
            .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
            .FirstOrDefaultAsync(o => o.ID == id);

            if (gantt == null)
            {
                return NotFound();
            }         
            
            return View(gantt);
        }

        

        //POST: GanttUpdate
        [HttpPost]
        public async Task<IActionResult> GanttUpdate(int id, OperationsSchedule model)
        {
            var ganttToUpdate = await _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .Include(o => o.Engineer)
                .Include(o => o.OperationsScheduleMachines).ThenInclude(o => o.MachineDescription)
                .Include(o => o.Note)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (ganttToUpdate == null)
            {
                return NotFound();
            }

            // Explicitly update the properties
            ganttToUpdate.ApprovalDrawingReleased = model.ApprovalDrawingReleased;
            ganttToUpdate.ApprovalDrawingReturned = model.ApprovalDrawingReturned;
            ganttToUpdate.EngineerPackageReleased = model.EngineerPackageReleased;
            ganttToUpdate.PreOrderReleased = model.PreOrderReleased;
            ganttToUpdate.PurchaseOrderDueDate = model.PurchaseOrderDueDate;
            ganttToUpdate.PUrchaseOrderRecieved = model.PUrchaseOrderRecieved;
            ganttToUpdate.ReadinessToShipActual = model.ReadinessToShipActual;

            try
            {
                _context.Update(ganttToUpdate);
                await _context.SaveChangesAsync();
                var allSchedules = await _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .Include(o => o.Engineer)
                .Include(o => o.OperationsScheduleMachines).ThenInclude(o => o.MachineDescription)
                .Include(o => o.Note)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor)
                .ToListAsync();

                return View("Gantt", allSchedules);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please see your system administrator.");
            }

            return View(ganttToUpdate);
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
        public async Task<IActionResult> UpdateTask([FromBody] OperationsSchedule updatedTask)
        {
            var existingTask = await _context.OperationsSchedules
              .FindAsync(updatedTask.ID);
            if (existingTask == null)
            {
                return NotFound();
            }

            // Update all the date properties
            existingTask.ApprovalDrawingExpected = updatedTask.ApprovalDrawingExpected;
            existingTask.ApprovalDrawingReleased = updatedTask.ApprovalDrawingReleased;
            existingTask.PreOrderExpected = updatedTask.PreOrderExpected;
            existingTask.PreOrderReleased = updatedTask.PreOrderReleased;
            existingTask.EngineerPackageExpected = updatedTask.EngineerPackageExpected;
            existingTask.EngineerPackageReleased = updatedTask.EngineerPackageReleased;
            existingTask.PurchaseOrderExpected = updatedTask.PurchaseOrderExpected;
            existingTask.PurchaseOrderDueDate = updatedTask.PurchaseOrderDueDate;
            existingTask.PUrchaseOrderRecieved = updatedTask.PUrchaseOrderRecieved;
            existingTask.ReadinessToShipExpected = updatedTask.ReadinessToShipExpected;
            existingTask.ReadinessToShipActual = updatedTask.ReadinessToShipActual;

            _context.OperationsSchedules.Update(existingTask);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Task updated successfully." });
        }
        #endregion

        // Helper method to apply date formatting to columns
        private void FormatDateColumn(ExcelWorksheet worksheet, int columnIndex, string columnName)
        {
            for (int row = 4; row <= worksheet.Dimension.End.Row; row++) // Starting from row 4, as row 3 contains headers
            {
                var cellValue = worksheet.Cells[row, columnIndex].Value;
                if (cellValue is DateTime dateValue)
                {
                    // Apply a date format if the value is a valid DateTime
                    worksheet.Cells[row, columnIndex].Style.Numberformat.Format = "yyyy-mm-dd";
                }
                else
                {
                    // If not a date, set it to "N/A" or handle any non-date values
                    worksheet.Cells[row, columnIndex].Value = "N/A";
                }
            }
        }

        private void PopulateAssignedVendorData(OperationsSchedule operationsSchedule)
        {
            var allOptions = _context.Vendors;
            var currentOptionsHS = new HashSet<int>(operationsSchedule.OperationsScheduleVendors.Select(b => b.VendorID));

            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var o in allOptions)
            {
                if (currentOptionsHS.Contains(o.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = o.ID,
                        DisplayText = o.VendorName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = o.ID,
                        DisplayText = o.VendorName
                    });
                }
            }
            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(o => o.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(o => o.DisplayText), "ID", "DisplayText");
        }

        private void UpdateOperationsScheduleVendors(string[] selectedOptions, OperationsSchedule operationsScheduleToUpdate)
        {
            if (selectedOptions == null)
            {
                operationsScheduleToUpdate.OperationsScheduleVendors = new List<OperationsScheduleVendor>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(operationsScheduleToUpdate.OperationsScheduleVendors.Select(b => b.VendorID));
            foreach (var o in _context.Vendors)
            {
                if (selectedOptionsHS.Contains(o.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(o.ID))
                    {
                        operationsScheduleToUpdate.OperationsScheduleVendors.Add(new OperationsScheduleVendor
                        {
                            VendorID = o.ID,
                            OperationsScheduleID = operationsScheduleToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(o.ID))
                    {
                        OperationsScheduleVendor? vendorToRemove = operationsScheduleToUpdate.OperationsScheduleVendors.FirstOrDefault(d => d.VendorID == o.ID);
                        if (vendorToRemove != null)
                        {
                            _context.Remove(vendorToRemove);
                        }
                    }
                }
            }
        }

        private bool OperationsScheduleExists(int id)
        {
            return _context.OperationsSchedules.Any(e => e.ID == id);
        }

        [HttpGet]
        public IActionResult GetVendors()
        {
            var vendors = _context.Vendors
                .Select(v => new SelectListItem
                {
                    Value = v.ID.ToString(),
                    Text = v.VendorName
                })
                .ToList();

            return Json(vendors);
        }



        [HttpPost]
        public async Task<IActionResult> CreateVendor(string vendorName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vendorName))
                {
                    return Json(new { success = false, message = "Vendor name is required" });
                }

                var vendor = new Vendor { VendorName = vendorName };
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    vendorId = vendor.ID,
                    vendorName = vendor.VendorName
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error creating vendor: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(string customerName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerName))
                {
                    return Json(new { success = false, message = "Customer name is required" });
                }

                var customer = new Customer { CustomerName = customerName };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    customerId = customer.ID,
                    customerName = customer.CustomerName
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error creating customer: " + ex.Message
                });
            }
        }
    }
}
