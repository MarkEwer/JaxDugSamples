using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_Procedure_To_Rules_Engine.ProceduralVersion
{
    public class TransactionProcessor
    {
        IGateway _gateway;
        public TransactionProcessor(IGateway gateway)
        {
            this._gateway = gateway;
        }

        public bool PerformFullCreditProcess(
            string cardNumber, 
            decimal amount, 
            out string authToken)
        {
            authToken = string.Empty;
            try
            {
                _gateway.ValidateCard(cardNumber);
            }
            catch(InvalidOperationException)
            {
                return false;
            }

            if (_gateway.CheckIfCreditAvailable(cardNumber, amount))
            {
                var id = _gateway.ReserveCredit(cardNumber, amount);
                if(id==Guid.Empty)
                {
                    return false;
                }
                else
                {
                    _gateway.LogReservation(cardNumber, id, amount);
                    authToken = _gateway.CompleteTransaction(cardNumber, id);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
