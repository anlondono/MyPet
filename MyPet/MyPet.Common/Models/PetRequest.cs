using System;
using System.ComponentModel.DataAnnotations;

namespace MyPet.Common.Models
{
    public class PetRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Description { get; set; }

        public string Race { get; set; }

        public int OwnerId { get; set; }

        public int PetTypeId { get; set; }

        public byte[] ImageArray { get; set; }

        public bool IsAvailable { get; set; }
    }
}
