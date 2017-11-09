using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Convert_Procedure_To_Rules_Engine.ProceduralVersion
{
    public class TestProdceduralVersion
    {
        IGateway _gateway;
        public TestProdceduralVersion()
        {
            this._gateway = new Gateway(Credit.GetSampleData());
        }

        [Fact(DisplayName = "Procedural - InvalidNumber")]
        public void InvalidNumberTest()
        {
            var cardNumber = "42";
            bool failed = false;
            string token = string.Empty;
            var sut = new TransactionProcessor(_gateway);
            try
            {
                sut.PerformFullCreditProcess(cardNumber, 500m, out token);
            }
            catch(InvalidOperationException)
            {
                failed = true;
            }

            Assert.True(failed);
        }

        [Fact(DisplayName = "Procedural - CreditAvailableTest")]
        public void CreditAvailableTest()
        {
            var cardNumber = "4800037508664675";
            string token = string.Empty;
            var sut = new TransactionProcessor(_gateway);

            sut.PerformFullCreditProcess(cardNumber, 500m, out token);

            Assert.NotEqual(string.Empty, token);
        }

        [Fact(DisplayName = "Procedural - CreditAvailableButReservedTest")]
        public void CreditAvailableButReservedTest()
        {
            var cardNumber = "4836803436404398198";
            string token = string.Empty;
            _gateway.ReserveCredit(cardNumber, 500m);
            var sut = new TransactionProcessor(_gateway);

            sut.PerformFullCreditProcess(cardNumber, 500m, out token);

            Assert.Equal(string.Empty, token);
        }

        [Fact(DisplayName = "Procedural - CreditNotAvailableTest")]
        public void CreditNotAvailableTest()
        {
            var cardNumber = "342844743236943";
            string token = string.Empty;
            _gateway.CompleteTransaction(cardNumber,
                _gateway.ReserveCredit(cardNumber, 500m));
            var sut = new TransactionProcessor(_gateway);

            sut.PerformFullCreditProcess(cardNumber, 500m, out token);

            Assert.Equal(string.Empty, token);
        }

        [Fact(DisplayName = "Procedural - RequestBeyondLimit")]
        public void RequestBeyondLimit()
        {
            var cardNumber = "5115490833902574";
            string token = string.Empty;
            var sut = new TransactionProcessor(_gateway);

            sut.PerformFullCreditProcess(cardNumber, 500m, out token);

            Assert.Equal(string.Empty, token);
        }
    }
}
