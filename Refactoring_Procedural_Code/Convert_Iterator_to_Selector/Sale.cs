namespace Convert_Iterator_to_Selector
{
    public class Sale
    {
        public Sale(string item, decimal amount, decimal coupons, int quantity)
        {
            this.Item = item;
            this.Amount = amount;
            this.Coupons = coupons;
            this.Quantity = quantity;
        }

        public string Item { get; }
        public decimal Amount {get;}
        public decimal Coupons {get;}
        public int Quantity { get; }
    }
}
