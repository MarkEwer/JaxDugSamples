using System;
using System.Collections.Generic;
using System.Linq;

namespace Convert_Procedure_To_Rules_Engine
{
    public class Credit
    {
        public string CardNumber { get; }
        public Credit(string cardNumber, decimal limit)
        {
            this.CardNumber = cardNumber;
            this.CreditLimit = limit;
            this.Reservations = new Dictionary<Guid, decimal>();
            this.Transactions = new Dictionary<Guid, decimal>();
        }

        public decimal CreditLimit { get; }
        public decimal Balance { get { return Transactions.Values.Sum(); } }
        public decimal AvailableCredit { get { return CreditLimit - Balance - ReservedCredit; } }
        public decimal ReservedCredit { get { return Reservations.Values.Sum(); } }
        public Dictionary<Guid, decimal> Reservations { get; }
        public Dictionary<Guid, decimal> Transactions { get; }

        public Guid Reserve(decimal amount)
        {
            var id = Guid.NewGuid();
            if(this.AvailableCredit >= amount)
            {
                this.Reservations.Add(id, amount);
            }
            return id;
        }
        public bool Process(Guid reservationId)
        {
            if(this.Reservations.ContainsKey(reservationId))
            {
                var res = this.Reservations[reservationId];
                this.Reservations.Remove(reservationId);
                this.Transactions.Add(reservationId, res);
                return true;
            }
            return false;
        }

        public static List<Credit> GetSampleData()
        {
            var list = new List<Credit>();
            list.Add(new Credit("4800037508664675", 1000m));
            list.Add(new Credit("4556581641389795", 750m));
            list.Add(new Credit("4836803436404398198", 830m));
            list.Add(new Credit("2720997257887838", 2500m));
            list.Add(new Credit("5292487944899140", 15m));
            list.Add(new Credit("5115490833902574", 90m));
            list.Add(new Credit("370284826698104", 990m));
            list.Add(new Credit("375739595942848", 660m));
            list.Add(new Credit("342844743236943", 650m));
            list.Add(new Credit("6011155905973398", 6450m));
            list.Add(new Credit("6011203774923670", 690m));
            list.Add(new Credit("6011207828552844421", 780m));
            return list;
        }
    }
}

















