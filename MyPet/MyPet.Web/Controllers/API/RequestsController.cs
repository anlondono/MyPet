using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Common.Models;
using MyPet.Web.Data;
using MyPet.Web.Data.Entities;

namespace MyPet.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Races
        [HttpGet]
        public IEnumerable<Request> GetRequests()
        {
            return _context.Requests.Where(r => r.Active);
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var race = await _context.Requests.FindAsync(id);

            if (race == null)
            {
                return NotFound();
            }

            return Ok(race);
        }

        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] AskingPetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            var adopter = await _context.Adopters.FindAsync(request.AdopterId);
            if (adopter == null)
            {
                return BadRequest("Not valid adopter.");
            }

            var pet = await _context.Pets.FindAsync(request.PetId);
            if (pet == null)
            {
                return BadRequest("Not valid pet.");
            }
            var houseType = await _context.HouseTypes.FindAsync(request.HouseTypeId);
            if (houseType == null)
            {
                return BadRequest("Not valid house type.");
            }

            var petRequest = new Request
            {
                Adopter = adopter,
                Active = true,
                Date = DateTime.UtcNow,
                HasKids = request.HasKids,
                HasOthePets = request.HasOthePets,
                HouseType = houseType,
                Observation = request.Observation,
                Pet = pet,
            };
            _context.Requests.Add(petRequest);
            await _context.SaveChangesAsync();
            return Ok(petRequest != null);
        }

        [HttpPost]
        public async Task<IActionResult> DenyRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            request.Active = false;
            request.Denied = true;
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            request.Active = true;
            request.Denied = false;
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}