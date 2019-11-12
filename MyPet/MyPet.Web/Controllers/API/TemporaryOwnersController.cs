using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Common.Models;
using MyPet.Web.Data;
using MyPet.Web.Data.Entities;
using MyPet.Web.Helpers;

namespace MyPet.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TemporaryOwnersController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public TemporaryOwnersController(DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("GetTemporaryOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userHelper.GetUserByEmailAsync(emailRequest.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                if (user.IsOwner)
                {
                    return await GetOwnerAsync(emailRequest);
                }
                else
                {
                    return await GetAdopterAsync(emailRequest);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<IActionResult> GetOwnerAsync(EmailRequest emailRequest)
        {
            var owner = await _dataContext.TemporaryOwners
                .Include(o => o.User)
                .Include(o => o.Pets)
                .ThenInclude(p => p.PetType)
                .Include(o => o.Pets)
                .ThenInclude(o => o.Requests)
                .ThenInclude(o => o.HouseType)
                .Include(o => o.Pets)
                .ThenInclude(o => o.Requests)
                .ThenInclude(o => o.Adopter)
                .ThenInclude(o => o.User)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

            var response = new TemporaryOwnerResponse
            {
                Id = owner.Id,
                IsAdopter = false,
                IsOwner = true,
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
                    IsAvailable = p.IsAvailable,
                    HistoryRequests = p.Requests.Select(h => new HistoryRequestResponse
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
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        private async Task<IActionResult> GetAdopterAsync(EmailRequest emailRequest)
        {
            var adopter = await _dataContext.Adopters
                .Include(o => o.User)
                .Include(o => o.Requests)
                .ThenInclude(p => p.Pet)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());
            var pets = await _dataContext.Pets
                .Include(pt => pt.PetType)
                .Include(pt => pt.Requests)
                .ThenInclude(o => o.HouseType)
                .Where(p => p.IsAvailable)
                .ToListAsync();
            var response = new TemporaryOwnerResponse
            {
                Id = adopter.Id,
                IsAdopter = true,
                IsOwner = false,
                FirstName = adopter.User.FirstName,
                LastName = adopter.User.LastName,
                Address = adopter.User.Address,
                Document = adopter.User.Document,
                Email = adopter.User.Email,
                PhoneNumber = adopter.User.PhoneNumber,
                Pets = pets.Select(p => new PetResponse
                {
                    Age = p.Age,
                    Id = p.Id,
                    ImageUrl = p.ImageFullPath,
                    Name = p.Name,
                    Race = p.Race,
                    Description = p.Description,
                    PetType = p.PetType.Name,
                    IsAvailable = p.IsAvailable,
                    HistoryRequests = p.Requests
                    .Where(where => where.Adopter!= null && where.Adopter.User.UserName.ToLower() == emailRequest.Email.ToLower())
                    .Select(h => new HistoryRequestResponse
                    {
                        Active = h.Active,
                        Adopter = adopter.User.FullName,
                        Date = h.Date,
                        Denied = h.Denied,
                        HasKids = h.HasKids,
                        HasPets = h.HasOthePets,
                        HouseType = h.HouseType.Name,
                        Observation = h.Observation,
                        Pet = p.Name,
                        RequestId = h.Id,
                        Telephone = adopter.User.CellPhone,
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }
    }
}
