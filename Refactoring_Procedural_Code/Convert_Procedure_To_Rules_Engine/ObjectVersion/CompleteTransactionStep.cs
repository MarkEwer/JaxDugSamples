using System;

namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class CompleteTransactionStep : Step, IProcessStep
    {
        public CompleteTransactionStep(IGateway gateway) : base(gateway) { }
        public bool Execute(CreditAuthorizationContext context)
        {
            context.AuthToken = _gateway.CompleteTransaction(
                context.CreditCardNumber,
                context.ReservationId);
            return context.AuthToken != String.Empty;
        }
    }
}
