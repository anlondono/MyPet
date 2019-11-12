using System;
using System.Collections.Generic;
using System.Text;

namespace MyPet.Common.Models
{
    public class HistoryRequestResponse
    {
        public int RequestId { get; set; }

        public DateTime Date { get; set; }

        public string Pet { get; set; }

        public string Adopter { get; set; }

        public string Telephone { get; set; }

        public bool HasKids { get; set; }

        public bool HasPets { get; set; }

        public string HouseType { get; set; }

        public string Observation { get; set; }

        public bool Active { get; set; }

        public bool Denied { get; set; }

        public string WasDenied  { get; set; }
        public string IsActive  { get; set; }
        public string HasKidsStr  { get; set; }
        public string HasPetsStr  { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();
    }
}
