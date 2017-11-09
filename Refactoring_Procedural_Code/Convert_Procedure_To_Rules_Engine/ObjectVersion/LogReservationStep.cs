namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class LogReservationStep : Step, IProcessStep
    {
        public LogReservationStep(IGateway gateway) : base(gateway) { }
        public bool Execute(CreditAuthorizationContext context)
        {
            _gateway.LogReservation(
                context.CreditCardNumber,
                context.ReservationId,
                context.Amount);
            return true;
        }
    }
}
