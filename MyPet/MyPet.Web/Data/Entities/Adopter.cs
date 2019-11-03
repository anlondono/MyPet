using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPet.Web.Data.Entities
{
    public class Adopter
    {
        public int Id { get; set; }
        public User User { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
