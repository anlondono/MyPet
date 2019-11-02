using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool HasKids { get; set; }

        public string Observation { get; set; }

        public bool HasOthePets { get; set; }

        public bool Active { get; set; }

        public bool Denied { get; set; }

        public HouseType HouseType { get; set; }

        public Adopter Adopter { get; set; }

        public Pet Pet { get; set; }

    }
}
