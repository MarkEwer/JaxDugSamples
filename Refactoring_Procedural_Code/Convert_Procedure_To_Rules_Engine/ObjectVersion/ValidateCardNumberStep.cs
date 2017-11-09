using System;

namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class ValidateCardNumberStep:Step, IProcessStep
    {
        public ValidateCardNumberStep(IGateway gateway) : base(gateway) { }
        public bool Execute(CreditAuthorizationContext context)
        {
            try
            {
                return _gateway.ValidateCard(context.CreditCardNumber);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
