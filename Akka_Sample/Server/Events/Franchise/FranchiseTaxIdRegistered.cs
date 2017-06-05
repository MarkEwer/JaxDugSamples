namespace Server.Events.Franchise
{
    public class FranchiseTaxIdRegistered
    {
        public FranchiseTaxIdRegistered(string franchiseId, string taxId)
        {
            this.FranchiseId = franchiseId;
            this.TaxId = taxId;
        }
        public string FranchiseId { get; }
        public string TaxId { get; }
    }
}
