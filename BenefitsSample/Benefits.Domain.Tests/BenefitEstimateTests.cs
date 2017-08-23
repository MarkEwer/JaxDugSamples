using Akka.Actor;
using Akka.TestKit.NUnit3;
using Benefits.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Tests
{
    [TestFixture]
    public class BenefitEstimateTests : TestKit
    {
        [Test]
        public void ValidateQuoteForEmployeeWithNoDependents()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");
            var test = this.CreateTestProbe();
            Sys.EventStream.Subscribe(test, typeof(Estimate));

            var benefit = Sys.ActorOf(Props.Create<BenefitEstimateActor>(id), $"Estimate-{id}");
            System.Threading.Thread.Sleep(1000);

            //Act
            AddEmployee(id, benefit);
            var estimate = test.ExpectMsg<Estimate>();
            
            SetSalary(id, benefit);
            var estimate2 = test.ExpectMsg<Estimate>();
            
            //Assert
            Assert.AreEqual(1000, estimate.AnnualCost);
            Assert.AreEqual(1000, estimate2.AnnualCost);
        }

        private void AddEmployee(string id, IActorRef benefit)
        {
            // Act
            var cmd = new Commands.AddEmployeeToBenefitsEstimate(id, "Mark", "Ewer", MaritalStatus.Single);
            benefit.Tell(cmd);

            // Assert
            var result = ExpectMsg<Domain.OperationResult>(new TimeSpan(0, 0, 15));
            Assert.True(result.IsSuccess);
        }

        private void SetSalary(string id, IActorRef benefit)
        {
            // Act
            var cmd = new Commands.SetEmployeeSalary(id, 26 * 2000, 26);
            benefit.Tell(cmd);

            // Assert
            var result = ExpectMsg<OperationResult>();
            Assert.True(result.IsSuccess);
        }
    }
}
