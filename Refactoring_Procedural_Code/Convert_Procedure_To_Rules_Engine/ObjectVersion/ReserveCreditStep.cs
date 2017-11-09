using System;

namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class ReserveCreditStep : Step, IProcessStep
    {
        public ReserveCreditStep(IGateway gateway) : base(gateway) { }
        public bool Execute(CreditAuthorizationContext context)
        {
            context.ReservationId = _gateway.ReserveCredit(
                context.CreditCardNumber,
                context.Amount);
            return context.ReservationId != Guid.Empty;
        }
    }
}
