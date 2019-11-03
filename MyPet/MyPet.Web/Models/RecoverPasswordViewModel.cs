using System.ComponentModel.DataAnnotations;

namespace MyPet.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
