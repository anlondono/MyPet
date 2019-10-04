using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters")]

        public string Name { get; set; }

        
        [Display(Name= "Aproximated Age")]
        public int Age { get; set; }

        public string Description { get; set; }

        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }

        public PetType PetType { get; set; }

        public PetImage PetImage { get; set; }

        public Race Race { get; set; }

        public ICollection<Request> Requests { get; set; }

        public TemporaryOwner TemporaryOwner { get; set; }
    }
}
