using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ValueObjects
{
    public class EntityId
    {
        public EntityId(string id) { this.Id = id; }
        public string Id { get; }
    }
}
