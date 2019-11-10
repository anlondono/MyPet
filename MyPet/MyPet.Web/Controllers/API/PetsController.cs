using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Common.Helpers;
using MyPet.Common.Models;
using MyPet.Web.Data;
using MyPet.Web.Data.Entities;

namespace MyPet.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PetsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PetsController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: api/Pets
        [HttpGet]
        public IEnumerable<Pet> GetPets()
        {
            return _dataContext.Pets.Where(p => p.IsAvailable);
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pet = await _dataContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        // PUT: api/Pets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet([FromRoute] int id, [FromBody] PetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            var oldPet = await _dataContext.Pets.FindAsync(request.Id);
            if (oldPet == null)
            {
                return BadRequest("Pet doesn't exists.");
            }

            var petType = await _dataContext.PetTypes.FindAsync(request.PetTypeId);
            if (petType == null)
            {
                return BadRequest("Not valid pet type.");
            }

            var imageUrl = oldPet.ImageUrl;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Pets";
                var fullPath = $"~/images/Pets/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            oldPet.ImageUrl = imageUrl;
            oldPet.Name = request.Name;
            oldPet.PetType = petType;
            oldPet.Race = request.Race;
            oldPet.Description = request.Description;
            oldPet.Age = request.Age;
            oldPet.IsAvailable = request.IsAvailable;

            _dataContext.Pets.Update(oldPet);
            await _dataContext.SaveChangesAsync();
            return Ok(oldPet != null);
        }

        // POST: api/Pets
        [HttpPost]
        public async Task<IActionResult> PostPet([FromBody] PetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var owner = await _dataContext.TemporaryOwners.FindAsync(request.OwnerId);
            if (owner == null)
            {
                return BadRequest("Not valid owner.");
            }

            var petType = await _dataContext.PetTypes.FindAsync(request.PetTypeId);
            if (petType == null)
            {
                return BadRequest("Not valid pet type.");
            }

            var imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Pets";
                var fullPath = $"~/images/Pets/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var pet = new Pet
            {
                IsAvailable = true,
                ImageUrl = imageUrl,
                Name = request.Name,
                TemporaryOwner = owner,
                PetType = petType,
                Race = request.Race,
                Description = request.Description,
                Age = request.Age,
            };

            _dataContext.Pets.Add(pet);
            await _dataContext.SaveChangesAsync();
            return Ok(pet != null);
        }


        private bool PetExists(int id)
        {
            return _dataContext.Pets.Any(e => e.Id == id);
        }
    }
}