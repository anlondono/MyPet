﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Common.Models;
using MyPet.Web.Data;

namespace MyPet.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TemporaryOwnersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TemporaryOwnersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetTemporaryOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var owner = await _dataContext.TemporaryOwners
                .Include(o => o.User)
                .Include(o => o.Pets)
                .ThenInclude(p => p.PetType)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

            var response = new TemporaryOwnerResponse
            {
                Id = owner.Id,
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                PhoneNumber = owner.User.PhoneNumber,
                Pets = owner.Pets.Select(p => new PetResponse
                {
                    Age = p.Age,
                    Id = p.Id,
                    ImageUrl = p.ImageFullPath,
                    Name = p.Name,
                    Race = p.Race,
                    Description = p.Description,
                    PetType = p.PetType.Name,
                }).ToList()
            };

            return Ok(response);
        }
    }
}