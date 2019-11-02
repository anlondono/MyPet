using MyPet.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();
            await CheckPetTypesAsync();
            await CheckPetsAsync();
            await CheckTemporaryOwner();
            await AddHouseType();
        }

        private async Task CheckTemporaryOwner()
        {
            if(!_context.TemporaryOwners.Any())
            {
                AddTemporaryOwners("102030453912",
                    "Andres","Londono","5854278","3225327305","Cll 122a");
                AddTemporaryOwners("1020304",
                   "Pedro", "Correa", "5854278", "3225327305", "Cll 122b");

            }
            
        }

        private void AddTemporaryOwners(string document,string firstname,
            string lastname,string fixedphone,string cellphone,string address)
        {
            _context.TemporaryOwners.Add(new TemporaryOwner
            {
               Document =document,
               FirstName =firstname,
               LastName=lastname,
               FixedPhone=fixedphone,
               CellPhone=cellphone,
               Address=address
               
            });
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
                Age=age,
                Description=description,
                IsAvailable=isavailable
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

    }
}
