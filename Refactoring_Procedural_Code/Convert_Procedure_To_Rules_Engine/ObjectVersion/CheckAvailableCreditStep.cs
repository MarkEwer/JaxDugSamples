namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class CheckAvailableCreditStep : Step, IProcessStep
    {
        public CheckAvailableCreditStep(IGateway gateway) : base(gateway) { }
        public bool Execute(CreditAuthorizationContext context)
        {
            return _gateway.CheckIfCreditAvailable(
                context.CreditCardNumber,
                context.Amount);
        }
    }
}
