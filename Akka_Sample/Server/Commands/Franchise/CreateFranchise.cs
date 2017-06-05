using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands.Franchise
{
    public class CreateFranchise
    {
        public Guid Id { get; protected set; }
        public CreateFranchise() : this(Guid.NewGuid()) { }
        public CreateFranchise(Guid id)
        {
            this.Id = id;
        }
    }
}
