﻿using System;
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
    public class MachineDescriptionController : Controller
    {
        private readonly HaverContext _context;

        public MachineDescriptionController(HaverContext context)
        {
            _context = context;
        }

        // GET: MachineDescription
        public async Task<IActionResult> Index()
        {
            return View(await _context.MachineDescriptions.ToListAsync());
        }

        // GET: MachineDescription/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineDescription = await _context.MachineDescriptions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (machineDescription == null)
            {
                return NotFound();
            }

            return View(machineDescription);
        }

        // GET: MachineDescription/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MachineDescription/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SerialNumber,Size,Class,Deck,NamePlateOrdered,NameplateRecieved,InstalledMedia,SparePartsSpareMedia,BaseFrame,AirSeal,CoatingLining,Disassembly")] MachineDescription machineDescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machineDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machineDescription);
        }

        //POST: MachineDescription/Create
        //Modal       
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CreateMachineModal([FromBody] MultiStepOperationsScheduleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new machine description from the view model
                    var newMachine = new MachineDescription
                    {
                        SerialNumber = viewModel.SerialNumber,
                        Size = viewModel.Size,
                        Class = viewModel.Class,
                        Deck = viewModel.Deck,
                        NamePlateOrdered = viewModel.NamePlateOrdered,
                        NameplateRecieved = viewModel.NameplateRecieved,
                        InstalledMedia = viewModel.InstalledMedia,
                        SparePartsSpareMedia = viewModel.SparePartsSpareMedia,
                        BaseFrame = viewModel.BaseFrame,
                        AirSeal = viewModel.AirSeal,
                        CoatingLining = viewModel.CoatingLining,
                        Disassembly = viewModel.Disassembly
                    };

                    // Add the new machine description to the database context
                    _context.MachineDescriptions.Add(newMachine);
                    _context.SaveChanges();

                    // Return the ID of the newly created machine
                    var newMachineID = newMachine.ID;

                    // Optionally, you could store the newMachineID in the session, TempData, or return it directly
                    return Json(new { success = true, newMachineDescription = newMachine.SerialNumber, newMachineID });
                }
                catch (Exception ex)
                {
                    // Handle errors
                    return Json(new { success = false, errorMessage = ex.Message });
                }
            }

            return BadRequest(new { success = false, errorMessage = "Invalid data." });
        }




        // GET: MachineDescription/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineDescription = await _context.MachineDescriptions.FindAsync(id);
            if (machineDescription == null)
            {
                return NotFound();
            }
            return View(machineDescription);
        }

        // POST: MachineDescription/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SerialNumber,Size,Class,Deck,NamePlateOrdered,NameplateRecieved,InstalledMedia,SparePartsSpareMedia,BaseFrame,AirSeal,CoatingLining,Disassembly")] MachineDescription machineDescription)
        {
            if (id != machineDescription.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineDescriptionExists(machineDescription.ID))
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
            return View(machineDescription);
        }

        // GET: MachineDescription/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineDescription = await _context.MachineDescriptions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (machineDescription == null)
            {
                return NotFound();
            }

            return View(machineDescription);
        }

        // POST: MachineDescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machineDescription = await _context.MachineDescriptions.FindAsync(id);
            if (machineDescription != null)
            {
                _context.MachineDescriptions.Remove(machineDescription);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineDescriptionExists(int id)
        {
            return _context.MachineDescriptions.Any(e => e.ID == id);
        }
    }
}
