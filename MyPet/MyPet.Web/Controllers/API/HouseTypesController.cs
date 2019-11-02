using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Web.Data;
using MyPet.Web.Data.Entities;

namespace MyPet.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public HouseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/HouseTypes
        [HttpGet]
        public IEnumerable<HouseType> GetHouseTypes()
        {
            return _context.HouseTypes;
        }

        // GET: api/HouseTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHouseType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var houseType = await _context.HouseTypes.FindAsync(id);

            if (houseType == null)
            {
                return NotFound();
            }

            return Ok(houseType);
        }
    }
}