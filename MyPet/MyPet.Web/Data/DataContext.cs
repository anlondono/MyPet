using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPet.Web.Data.Entities;

namespace MyPet.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TemporaryOwner> TemporaryOwners { get; set; }
        public DbSet<Adopter> Adopters { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }

    }
}
