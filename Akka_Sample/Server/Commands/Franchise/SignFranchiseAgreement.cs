using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands.Franchise
{
    public class SignFranchiseAgreement
    {
        public SignFranchiseAgreement(string franchiseId, DateTime signingDate)
        {
            this.FranchiseId = franchiseId;
            this.AgreementSigningDate = signingDate;
        }
        public string FranchiseId { get; }
        public DateTime AgreementSigningDate { get; }
    }
}
