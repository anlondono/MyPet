using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RequestsController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        [Route("Adopter/{id}")]
        public IEnumerable<HistoryRequestResponse> GetRequestsAdopter([FromRoute] int id)
        {
            var data = _context.Requests.Where(r => r.Adopter.Id == id)
                .Include(o => o.Pet)
                .Include(o=> o.HouseType)
                .Include(o => o.Adopter)
                .ThenInclude(o => o.User);
            return data.Select(h => new HistoryRequestResponse
            {
                Active = h.Active,
                Adopter = h.Adopter.User.FullName,
                Date = h.Date,
                Denied = h.Denied,
                HasKids = h.HasKids,
                HasPets = h.HasOthePets,
                HouseType = h.HouseType.Name,
                Observation = h.Observation,
                Pet = h.Pet.Name,
                RequestId = h.Id,
                Telephone = h.Adopter.User.CellPhone,
            }).ToList();
        }
        // GET: api/Requests
        [HttpGet]
        [Route("Owner/{id}")]
        public IEnumerable<HistoryRequestResponse> GetRequestsOwner([FromRoute] int id)
        {
            var data = _context.Requests
                .Where(r => r.Pet.TemporaryOwner.Id == id)
                .Include(o => o.Pet)
                .Include(o => o.HouseType)
                .Include(o => o.Adopter)
                .ThenInclude(o => o.User);
            return data.Select(h => new HistoryRequestResponse
            {
                Active = h.Active,
                Adopter = h.Adopter.User.FullName,
                Date = h.Date,
                Denied = h.Denied,
                HasKids = h.HasKids,
                HasPets = h.HasOthePets,
                HouseType = h.HouseType.Name,
                Observation = h.Observation,
                Pet = h.Pet.Name,
                RequestId = h.Id,
                Telephone = h.Adopter.User.CellPhone,
            }).ToList();
        }

        [HttpPost]
        [Route("Create")]
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
        [Route("Deny/{id}")]
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
        [Route("Accept/{id}")]
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