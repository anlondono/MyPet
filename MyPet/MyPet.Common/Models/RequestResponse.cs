using System;
using System.Collections.Generic;
using System.Text;

namespace MyPet.Common.Models
{
    public class RequestResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool HasKids { get; set; }

        public string Observation { get; set; }

        public bool HasOthePets { get; set; }

        public bool Active { get; set; }

        public bool Denied { get; set; }

        public string HouseType { get; set; }

        public string Adopter { get; set; }

        public PetResponse Pet { get; set; }
    }
}
