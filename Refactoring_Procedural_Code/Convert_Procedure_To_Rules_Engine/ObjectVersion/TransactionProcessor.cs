using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public class TransactionProcessor
    {
        public List<IProcessStep> _steps;
        private IGateway _gateway;
        public TransactionProcessor(IGateway gateway)
        {
            this._gateway = gateway;
            this._steps = new List<IProcessStep>();
            this.SetupSteps();
        }

        public bool PerformFullCreditProcess(
            string cardNumber,
            decimal amount,
            out string authToken)
        {
            var context = new CreditAuthorizationContext
            {
                CreditCardNumber = cardNumber,
                Amount = amount
            };

            foreach (var step in _steps)
            {
                if (!step.Execute(context)) break;
            }

            authToken = context.AuthToken;
            return false;
        }

        private void SetupSteps()
        {
            this._steps.Add(new ValidateCardNumberStep(this._gateway));
            this._steps.Add(new CheckAvailableCreditStep(this._gateway));
            this._steps.Add(new ReserveCreditStep(this._gateway));
            this._steps.Add(new LogReservationStep(this._gateway));
            this._steps.Add(new CompleteTransactionStep(this._gateway));
        }
    }
}
