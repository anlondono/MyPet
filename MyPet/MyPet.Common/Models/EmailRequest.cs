using System.ComponentModel.DataAnnotations;

namespace MyPet.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
