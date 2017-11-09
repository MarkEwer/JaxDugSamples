using System;

namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class CreditAuthorizationContext
    {
        public string CreditCardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0m;
        public Guid ReservationId { get; set; } = Guid.Empty;
        public string AuthToken { get; set; } = string.Empty;
        public bool Approved { get; set; } = false;
    }
}
