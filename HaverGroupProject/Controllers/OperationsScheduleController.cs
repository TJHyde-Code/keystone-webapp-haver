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
        public IActionResult Step1()
        {
            var viewModel = new MultiStepOperationsScheduleViewModel();
            return View(viewModel);
        }

        //This saves the partial data and redirects to Step 2
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step1(MultiStepOperationsScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = new OperationsSchedule
                {
                    SalesOrdNum = model.SalesOrdNum
                };

                _context.OperationsSchedules.Add(operationsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction("Step2", new { id = operationsSchedule.ID });
            }
            return View(model);
        }

        //STEP 2
        //This loads the form to select or create a customer
        //GET
        public async Task<IActionResult> Step2(int id)
        {
            var operationsSchedule = await _context.OperationsSchedules.FindAsync(id);
            if (operationsSchedule == null) return NotFound();

            var viewModel = new MultiStepOperationsScheduleViewModel
            {
                ID = operationsSchedule.ID,
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
        public async Task<IActionResult> Step2(MultiStepOperationsScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules.FindAsync(model.ID);
                if (operationsSchedule == null) return NotFound();

                operationsSchedule.CustomerID = model.CustomerID;
                _context.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction("Step3", new { id = operationsSchedule.ID });
            }
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
        public async Task<IActionResult> Step3(MultiStepOperationsScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules.FindAsync(model.ID);
                if (operationsSchedule == null) return NotFound();

                operationsSchedule.MachineDescriptionID = model.MachineDescriptionID ?? 0;
                _context.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction("Step4", new { id = operationsSchedule.ID });
            }
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
                Vendors = _context.Vendors
                    .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.VendorName })
                    .ToList(),
                SelectedVendorIDs = operationsSchedule.OperationsScheduleVendors
                    .Select(ov => ov.VendorID.ToString()).ToArray()
            };
            return View(viewModel);
        }


        //This saves the partial data and redirects to the index page
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step4(MultiStepOperationsScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationsSchedule = await _context.OperationsSchedules
                    .Include(o => o.OperationsScheduleVendors)
                    .FirstOrDefaultAsync(o => o.ID == model.ID);

                if (operationsSchedule == null) return NotFound();

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
                _context.Update(operationsSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult DownloadOperationsSchedules()
        {
            //Get the appointments
            var schedules = from os in _context.OperationsSchedules
                        .Include(os => os.Customer)
                        .Include(os => os.Vendor)
                            orderby os.KickoffMeeting descending
                            select new
                            {
                                SalesOrder = os.SalesOrdNum,
                                Customer = os.Customer.CustomerName,
                                Date = os.KickoffMeeting.ToString("yyyy-MM-dd"),
                                Machine = os.MachineDesc,
                                SerialNumber = os.SerialNum,
                                Engineer = os.PackageReleaseName,
                                Vendor = os.Vendor.VendorName,
                                PONum = os.PONum,
                                DeliveryDate = os.DeliveryDate.ToString("yyyy-MM-dd")

                            };
            //How many rows?
            int numRows = schedules.Count();

            if (numRows > 0) //We have data
            {
                //Create a new spreadsheet from scratch.
                using (ExcelPackage excel = new ExcelPackage())
                {

                    //Note: you can also pull a spreadsheet out of the database if you
                    //have saved it in the normal way we do, as a Byte Array in a Model
                    //such as the UploadedFile class.
                    //
                    // Suppose...
                    //
                    // var theSpreadsheet = _context.UploadedFiles.Include(f => f.FileContent).Where(f => f.ID == id).SingleOrDefault();
                    //
                    //    //Pass the Byte[] FileContent to a MemoryStream
                    //
                    // using (MemoryStream memStream = new MemoryStream(theSpreadsheet.FileContent.Content))
                    // {
                    //     ExcelPackage package = new ExcelPackage(memStream);
                    // }

                    var workSheet = excel.Workbook.Worksheets.Add("OperationsSchedules");

                    //Note: Cells[row, column]
                    workSheet.Cells[3, 1].LoadFromCollection(schedules, true);

                    //Style first column for dates
                    workSheet.Column(1).Style.Numberformat.Format = "yyyy-mm-dd";

                    //Style fee column for currency
                    workSheet.Column(8).Style.Numberformat.Format = "yyyy-MM-dd";

                    //Note: these are fine if you are only 'doing' one thing to the range of cells.
                    //Otherwise you should USE a range object for efficiency
                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 9])//
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
