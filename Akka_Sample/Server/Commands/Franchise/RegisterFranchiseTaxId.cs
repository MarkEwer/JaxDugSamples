using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands.Franchise
{
    public class RegisterFranchiseTaxId
    {
        public RegisterFranchiseTaxId(string franchiseId, string taxId)
        {
            this.FranchiseId = franchiseId;
            this.TaxId = taxId;
        }
        public string FranchiseId { get; }
        public string TaxId { get; }
    }
}
