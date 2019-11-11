namespace MyPet.Common.Models
{
    public class PetResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string ImageUrl { get; set; }

        public string Race { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public string PetType { get; set; }
    }
}
