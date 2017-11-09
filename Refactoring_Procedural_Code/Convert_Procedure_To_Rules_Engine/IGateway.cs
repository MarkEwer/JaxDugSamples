using System;
using System.Text;

namespace Convert_Procedure_To_Rules_Engine
{
    public interface IGateway
    {
        bool ValidateCard(string cardNumber);
        bool CheckIfCreditAvailable(string cardNumber, decimal amount);
        Guid ReserveCredit(string cardNumber, decimal amount);
        void LogReservation(string cardNumber, Guid reservationId, decimal amount);
        string CompleteTransaction(string cardNumber, Guid reservationId);
    }
}
