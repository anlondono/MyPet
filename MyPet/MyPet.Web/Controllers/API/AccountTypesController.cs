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
    public class AccountTypesController : Controller
    {
        private readonly DataContext _dataContext;

        public AccountTypesController(DataContext context)
        {
            _dataContext = context;
        }

        [HttpPost]
        [Route("GetAccountTypeByEmail")]
        public async Task<IActionResult> GetAccount(EmailRequest emailRequest)
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
            if (owner != null)
            {
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
            var adopter = await _dataContext.Adopters
              .Include(o => o.User)
              .Include(o => o.Requests)
              .ThenInclude(p => p.Pet)
              .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

            return Ok(adopter);

        }
    }
}