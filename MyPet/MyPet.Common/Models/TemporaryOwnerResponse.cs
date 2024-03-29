﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPet.Common.Models
{
    public class TemporaryOwnerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsOwner { get; set; }

        public bool IsAdopter { get; set; }

        public ICollection<PetResponse> Pets { get; set; }

        public ICollection<RequestResponse> Requests { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
