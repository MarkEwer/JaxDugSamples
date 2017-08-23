namespace Benefits.Domain.Models
{
    public struct Person
    {
        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private decimal DiscountRate
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
        public decimal ApplyDiscountRate(decimal baseCost)
        {
            return this.DiscountRate * baseCost;
        }
    }
}
