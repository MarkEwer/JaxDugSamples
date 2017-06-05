using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ValueObjects
{
    public struct Person
    {
        public Person (string givenName, string surname)
        {
            this.GivenName = givenName;
            this.Surname = surname;
        }
        public string GivenName { get; }
        public string Surname { get; }
    }
}
