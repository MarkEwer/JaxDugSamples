using System;
using System.Collections.Generic;
using System.Linq;

namespace Convert_Procedure_To_Rules_Engine
{
    public class Gateway:IGateway
    {
        readonly Dictionary<string, Credit> _cards;

        public Gateway()
        {
            this._cards = new Dictionary<string, Credit>();
        }

        public Gateway(List<Credit> accounts)
        {
            this._cards = new Dictionary<string, Credit>();
            accounts.ForEach(x => _cards.Add(x.CardNumber, x));
        }

        public bool ValidateCard(string cardNumber)
        {
            return this._cards.ContainsKey(cardNumber);
        }

        public bool CheckIfCreditAvailable(string cardNumber, decimal amount)
        {
            if (!this._cards.ContainsKey(cardNumber)) throw new InvalidOperationException("Invalid Card Number");
            return amount <= this._cards[cardNumber].AvailableCredit;
        }

        public Guid ReserveCredit(string cardNumber, decimal amount)
        {
            if (!this._cards.ContainsKey(cardNumber)) throw new InvalidOperationException("Invalid Card Number");
            var id = this._cards[cardNumber].Reserve(amount);
            return id;
        }

        public void LogReservation(string cardNumber, Guid reservationId, decimal amount) { }
        public string CompleteTransaction(string cardNumber, Guid reservationId)
        {
            if (!this._cards.ContainsKey(cardNumber)) throw new InvalidOperationException("Invalid Card Number");
            if (this._cards[cardNumber].Process(reservationId))
                return reservationId.ToString("N");
            else
                return string.Empty;
        }
    }
}
