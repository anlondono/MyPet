using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPet.Web.Data;
using MyPet.Web.Data.Entities;

namespace MyPet.Web.Controllers
{
    public class TemporaryOwnersController : Controller
    {
        private readonly DataContext _context;

        public TemporaryOwnersController(DataContext context)
        {
            _context = context;
        }

        // GET: TemporaryOwners
        public async Task<IActionResult> Index()
        {
            return View(await _context.TemporaryOwners.ToListAsync());
        }

        // GET: TemporaryOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporaryOwner = await _context.TemporaryOwners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temporaryOwner == null)
            {
                return NotFound();
            }

            return View(temporaryOwner);
        }

        // GET: TemporaryOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TemporaryOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document,FirstName,LastName,FixedPhone,CellPhone,Address")] TemporaryOwner temporaryOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(temporaryOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(temporaryOwner);
        }

        // GET: TemporaryOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporaryOwner = await _context.TemporaryOwners.FindAsync(id);
            if (temporaryOwner == null)
            {
                return NotFound();
            }
            return View(temporaryOwner);
        }

        // POST: TemporaryOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Document,FirstName,LastName,FixedPhone,CellPhone,Address")] TemporaryOwner temporaryOwner)
        {
            if (id != temporaryOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(temporaryOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemporaryOwnerExists(temporaryOwner.Id))
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
            return View(temporaryOwner);
        }

        // GET: TemporaryOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporaryOwner = await _context.TemporaryOwners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temporaryOwner == null)
            {
                return NotFound();
            }

            return View(temporaryOwner);
        }

        // POST: TemporaryOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temporaryOwner = await _context.TemporaryOwners.FindAsync(id);
            _context.TemporaryOwners.Remove(temporaryOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemporaryOwnerExists(int id)
        {
            return _context.TemporaryOwners.Any(e => e.Id == id);
        }
    }
}
