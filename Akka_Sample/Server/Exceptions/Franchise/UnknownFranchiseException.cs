using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Exceptions.Franchise
{
    public class UnknownFranchiseException : Exception
    {
        public UnknownFranchiseException(string message) : base(message) { }
        public UnknownFranchiseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
