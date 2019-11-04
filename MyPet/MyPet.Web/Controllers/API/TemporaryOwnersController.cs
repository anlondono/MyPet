using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPet.Common.Models;
using MyPet.Web.Data;
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

                if (await _userHelper.IsUserInRoleAsync(user, "Owner"))
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

        private async Task<IActionResult> GetAdopterAsync(EmailRequest emailRequest)
        {
            var adopter = await _dataContext.Adopters
                .Include(o => o.User)
                .Include(o => o.Requests)
                .ThenInclude(p => p.Pet)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

            var response = new TemporaryOwnerResponse
            {
                Id = adopter.Id,
                FirstName = adopter.User.FirstName,
                LastName = adopter.User.LastName,
                Address = adopter.User.Address,
                Document = adopter.User.Document,
                Email = adopter.User.Email,
                PhoneNumber = adopter.User.PhoneNumber,
                Requests = adopter.Requests.Select(r => new RequestResponse
                {
                    Id = r.Id,
                    Pet = new PetResponse
                    {
                        Age = r.Pet.Age,
                        Id = r.Pet.Id,
                        ImageUrl = r.Pet.ImageFullPath,
                        Name = r.Pet.Name,
                        Race = r.Pet.Race,
                        Description = r.Pet.Description,
                        PetType = r.Pet.PetType.Name,
                    },
                    Date = r.Date,
                    Adopter = r.Adopter.User.FullName,
                    HasKids = r.HasKids,
                    HasOthePets = r.HasOthePets,
                    HouseType = r.HouseType.Name,
                    Observation = r.Observation,
                    Active = r.Active,
                    Denied = r.Denied,
                }).ToList()
            };

            return Ok(response);
        }
    }
}
