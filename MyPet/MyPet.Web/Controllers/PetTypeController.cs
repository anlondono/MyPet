using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Web.Data;
using MyPet.Web.Data.Entities;

namespace MyPet.Web.Controllers
{
    public class PetTypeController : Controller
    {
        private readonly DataContext _context;
        public PetTypeController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.HouseTypes.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PetType petType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petType);
        }
    }
}