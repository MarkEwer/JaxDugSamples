using Benefits.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Events
{
    public class EstimateCreated : EventBase
    {
        public EstimateCreated(string id, string firstName, string lastName):base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
    }
}
