namespace Convert_Algorithm_to_Strategy
{
    public class Sale
    {
        public Sale(string item, decimal amount, decimal donations, int quantity)
        {
            this.Item = item;
            this.Amount = amount;
            this.Donations = donations;
            this.Quantity = quantity;
        }

        public string Item { get; }
        public decimal Amount {get;}
        public decimal Donations {get;}
        public int Quantity { get; }
    }
}
