using MyPet.Web.Data.Entities;
using MyPet.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            this._userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await CheckRoles();
            await _context.Database.EnsureCreatedAsync();
            await CheckPetTypesAsync();
            await CheckPetsAsync();
            await AddHouseType();
            var manager = await CheckUserAsync("1010", "Juan", "Pruebas", "super@gmail.com", "350 634 2747", "Calle Luna Calle Sol", "Admin");
            var owner = await CheckUserAsync("2020", "Dueño", "Pruebas", "owner@yopmail.com",
                "350 634 2747", "Calle Luna Calle Sol", "Owner", true, false);
            var adopter = await CheckUserAsync("3030", "Adoptante", "Pruebas", "adoptante@gmail.com",
                "350 634 2747", "Calle Luna Calle Sol", "Adopter", false, true);
            await CheckTemporaryOwner(owner);
            await CheckAdopter(adopter);
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Adopter");
        }

        private async Task CheckTemporaryOwner(User user)
        {
            if (!_context.TemporaryOwners.Any())
            {
                _context.TemporaryOwners.Add(new TemporaryOwner { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAdopter(User user)
        {
            if (!_context.Adopters.Any())
            {
                _context.Adopters.Add(new Adopter { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPetsAsync()
        {
            if (!_context.Pets.Any())
            {
                AddPets("Firulais", 4, "sgasdgsdsgagdas", true);
                AddPets("Congo", 4, "sgasdgsdsgagdas", false);
                AddPets("Tony", 4, "sgadsaagagdas", true);
                AddPets("Pepe", 4, "sgadsgaagdas", true);
                AddPets("Misifu", 4, "sgadsgagdas", true);
                await _context.SaveChangesAsync();
            }
        }

        private void AddPets(string name, int age, string description, bool isavailable)
        {
            _context.Pets.Add(new Pet
            {

                Name = name,
                Age = age,
                Description = description,
                IsAvailable = isavailable
            });
        }

        private async Task CheckPetTypesAsync()
        {
            if (!_context.PetTypes.Any())
            {
                _context.PetTypes.Add(new PetType { Name = "Dog" });
                _context.PetTypes.Add(new PetType { Name = "Cat" });
                _context.PetTypes.Add(new PetType { Name = "rabbit" });
                _context.PetTypes.Add(new PetType { Name = "parrot" });
                _context.PetTypes.Add(new PetType { Name = "snake" });
                await _context.SaveChangesAsync();
            }


        }

        private async Task AddHouseType()
        {
            if (!_context.HouseTypes.Any())
            {
                _context.HouseTypes.Add(new HouseType { Name = "House" });
                _context.HouseTypes.Add(new HouseType { Name = "Apartament" });
                _context.HouseTypes.Add(new HouseType { Name = "Farm" });
            }
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string role,
            bool isOwner = false,
            bool isAdopter = false)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    CellPhone = phone,
                    IsOwner = isOwner,
                    IsAdopter = isAdopter
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

    }
}
