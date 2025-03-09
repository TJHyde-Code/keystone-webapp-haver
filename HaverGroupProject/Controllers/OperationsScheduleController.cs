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
        public async Task<IActionResult> Index()
        {
                var haverContext = _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.Vendor)
                .Include(o => o.Engineer)
                .Include(o => o.MachineDescription)
                .Include(o => o.Note)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(d => d.Vendor);
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
                .Include(o => o.Engineer)
                .Include(o => o.MachineDescription)
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
            if (id == null)
            {
                return NotFound();
            }

            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.OperationsScheduleVendors).ThenInclude(o => o.Vendor)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (operationsSchedule == null)
            {
                return NotFound();
            }

            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "CustomerName", operationsSchedule.CustomerID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", operationsSchedule.VendorID);

            PopulateAssignedVendorData(operationsSchedule);
            return View(operationsSchedule);
        }

        // POST: OperationsSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SalesOrdNum,ExtSalesOrdNum,CustomerID,MachineDesc,SerialNum,PackageReleaseName,KickoffMeeting,ReleaseApprovalDrawing,VendorID,PONum,PODueDate,DeliveryDate,InstalledMedia,SparePartsSpareMedia,BaseFrame,AirSeal,CoatingLining,Disassebmly")] OperationsSchedule operationsSchedule, string[] selectedOptions)
        {
            var operationsScheduleToUpdate = await _context.OperationsSchedules
                .Include(o => o.Customer)
                .Include(o => o.OperationsScheduleVendors).ThenInclude(o => o.Vendor)
                .FirstOrDefaultAsync (o => o.ID == id);

            if (operationsScheduleToUpdate == null)
            {
                return NotFound();
            }

            UpdateOperationsScheduleVendors(selectedOptions, operationsScheduleToUpdate);

            if (await TryUpdateModelAsync<OperationsSchedule>(operationsScheduleToUpdate, "", o => o.Vendor))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { operationsScheduleToUpdate.ID });
                }
                catch(RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attemtps.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationsScheduleExists(operationsScheduleToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", operationsScheduleToUpdate.CustomerID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "ID", "ID", operationsScheduleToUpdate.VendorID);

            PopulateAssignedVendorData(operationsScheduleToUpdate);
            return View(operationsScheduleToUpdate);
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
                    ExtSalesOrdNum = operationsSchedule.ExtSalesOrdNum,
                    PackageReleaseName = operationsSchedule.PackageReleaseName,
                    KickoffMeeting = operationsSchedule.KickoffMeeting,
                    ReleaseApprovalDrawing = operationsSchedule.ReleaseApprovalDrawing
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
                        ExtSalesOrdNum = model.ExtSalesOrdNum,
                        PackageReleaseName = model.PackageReleaseName,
                        KickoffMeeting = model.KickoffMeeting,
                        ReleaseApprovalDrawing = model.ReleaseApprovalDrawing
                    };
                    _context.OperationsSchedules.Add(operationsSchedule);
                }
                else
                {
                    operationsSchedule.SalesOrdNum = model.SalesOrdNum;
                    operationsSchedule.ExtSalesOrdNum = model.ExtSalesOrdNum;
                    operationsSchedule.PackageReleaseName = model.PackageReleaseName;
                    operationsSchedule.KickoffMeeting = model.KickoffMeeting;
                    operationsSchedule.ReleaseApprovalDrawing = model.ReleaseApprovalDrawing;

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
                    .ToList()
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

                _context.OperationsSchedules.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Step3", new { id = operationsSchedule.ID });
            }
            model.Customers = _context.Customers
                .Select(c => new SelectListItem {  Value = c.ID.ToString(), Text = c.CustomerName })
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
            model.Machines = _context.MachineDescriptions
                .Select(m => new SelectListItem { Value = m.ID.ToString(), Text = $"{m.SerialNumber} - {m.Size} - {m.Class} - {m.Deck}"})
                .ToList();

            return View(model);
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
                PODueDate = operationsSchedule.PODueDate,
                DeliveryDate = operationsSchedule.DeliveryDate

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
                operationsSchedule.PODueDate = model.PODueDate;
                operationsSchedule.DeliveryDate = model.DeliveryDate;

                _context.OperationsSchedules.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Step5", new { id = operationsSchedule.ID });
            }
            model.Vendors = _context.Vendors
                .Select(v => new SelectListItem {  Value = v.ID.ToString(), Text = v.VendorName })
                .ToList();

            return View(model);
        }

        //STEP 5
        //This loads the form to update machineDescription bool fields and add notes fields
        //GET
        public async Task<IActionResult> Step5(int id)
        {
            var operationsSchedule = await _context.OperationsSchedules
                .Include(o => o.MachineDescription)
                .Include(o => o.Note)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
                NamePlateOrdered = operationsSchedule.MachineDescription?.NamePlateOrdered ?? false,
                NameplateRecieved = operationsSchedule.MachineDescription?.NameplateRecieved ?? false,
                InstalledMedia = operationsSchedule.MachineDescription?.InstalledMedia ?? false,
                SparePartsSpareMedia = operationsSchedule.MachineDescription?.SparePartsSpareMedia ?? false,
                BaseFrame = operationsSchedule.MachineDescription?.BaseFrame ?? false,
                AirSeal = operationsSchedule.MachineDescription?.AirSeal ?? false,
                CoatingLining = operationsSchedule.MachineDescription?.CoatingLining ?? false,
                Disassembly = operationsSchedule.MachineDescription?.Disassembly ?? false,

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
                    .Include(o => o.MachineDescription)
                    .Include(o => o.Note)
                    .FirstOrDefaultAsync(o => o.ID == model.ID);

                if (operationsSchedule == null) return NotFound();

                if (operationsSchedule.MachineDescription != null)
                {
                    operationsSchedule.MachineDescription.NamePlateOrdered = model.NamePlateOrdered;
                    operationsSchedule.MachineDescription.NameplateRecieved = model.NameplateRecieved;
                    operationsSchedule.MachineDescription.InstalledMedia = model.InstalledMedia;
                    operationsSchedule.MachineDescription.SparePartsSpareMedia = model.SparePartsSpareMedia;
                    operationsSchedule.MachineDescription.BaseFrame = model.BaseFrame;
                    operationsSchedule.MachineDescription.AirSeal = model.AirSeal;
                    operationsSchedule.MachineDescription.CoatingLining = model.CoatingLining;
                    operationsSchedule.MachineDescription.Disassembly = model.Disassembly;
                }

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
                                Engineer = os.PackageReleaseName,
                                Vendor = os.Vendor.VendorName,
                                PO_Num = os.PONum,
                                DeliveryDate = os.DeliveryDate.HasValue ? os.DeliveryDate.Value : (DateTime?)null // Keep as DateTime
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
    }
}
