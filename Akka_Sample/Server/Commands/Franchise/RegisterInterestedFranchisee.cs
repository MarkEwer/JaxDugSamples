using Server.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands.Franchise
{
    public class RegisterInterestedFranchisee
    {
        public RegisterInterestedFranchisee(
            string name, string givenName, string surname, StreetAddress billingAddress)
        {
            this.Name = name;
            this.GivenName = givenName;
            this.Surname = surname;
            this.BillingAddress = billingAddress;
        }
        public string Name { get; }
        public string GivenName { get; }
        public string Surname { get; }
        public StreetAddress BillingAddress { get; }
    }
}
