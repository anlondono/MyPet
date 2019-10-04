using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data.Entities
{
    public class PetImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TDB.azurewebsites.net{ImageUrl.Substring(1)}";

        public ICollection<PetImage> PetImages { get; set; }
    }
}