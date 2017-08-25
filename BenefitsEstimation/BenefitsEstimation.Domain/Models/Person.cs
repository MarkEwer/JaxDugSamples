namespace Benefits.Domain.Models
{
    public struct Person
    {
        public Person(string firstName, string lastName, decimal baseCost)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BaseCost = baseCost;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public decimal DiscountRate
        {
            get
            {
                if ((this.FirstName.StartsWith("A", System.StringComparison.OrdinalIgnoreCase))
                    || (this.LastName.StartsWith("A", System.StringComparison.OrdinalIgnoreCase)))
                {
                    return Config.DiscountRateForACustomers;
                }
                else
                {
                    return Config.DefaultDiscountRate;
                }
            }
        }
        public decimal BaseCost { get; private set; }
        public decimal ActualCost { get { return this.ApplyDiscountRate(this.BaseCost); } }
        public decimal ApplyDiscountRate(decimal baseCost)
        {
            return this.DiscountRate * baseCost;
        }
    }
}
