using System;
using System.Collections.Generic;
using System.Text;

namespace MyPet.Common.Models
{
    public class AskingPetRequest
    {

        public bool HasKids { get; set; }
        public bool HasOthePets { get; set; }

        public string Observation { get; set; }

        public int HouseTypeId { get; set; }

        public int AdopterId { get; set; }

        public int PetId { get; set; }
    }
}
