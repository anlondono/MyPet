using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data.Entities
{
    public class Race
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Type Race")]
        [MaxLength(20,ErrorMessage ="The {0} field can not have more than {1} characters")]
        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
