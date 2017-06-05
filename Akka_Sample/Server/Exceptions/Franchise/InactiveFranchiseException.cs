using System;

namespace Server.Exceptions.Franchise
{
    public class InactiveFranchiseException : Exception
    {
        public InactiveFranchiseException(string message) : base(message) { }
        public InactiveFranchiseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
