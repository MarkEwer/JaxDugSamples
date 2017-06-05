using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ValueObjects
{
    public class StreetAddress
    {
        public StreetAddress(string street, string city, string state, string postalCode, string country="")
        {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;
        }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string PostalCode { get; }
        public string Country { get; }
    }
}
