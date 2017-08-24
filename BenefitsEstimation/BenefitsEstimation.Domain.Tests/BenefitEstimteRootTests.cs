using Benefits.Domain.Models;
using d60.Cirqus;
using d60.Cirqus.Ntfs.Config;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Tests
{
    [TestFixture]
    public class BenefitEstimteRootTests
    {
        private const string EVENTS_FOLDER = "UnitTestEvents";
        private ICommandProcessor _processor;

        [SetUp] public void StartDomain()
        {
            var path = this.GetType().Assembly.Location;
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = Path.Combine(path, EVENTS_FOLDER);

            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            _processor = CommandProcessor.With()
                .EventStore(c => c.UseFiles(path))
                //.EventDispatcher(c => c.UseViewManagerEventDispatcher(viewManagers))
                .Create();
        }
        [TearDown] public void StopDomain()
        {
            _processor.Dispose();
        }

        [Test]
        public void ValidateQuoteForEmployeeWithNoDependents()
        { 
            // Arrange
            var id = Guid.NewGuid().ToString("N");

            //Act
            AddEmployee(id);
            SetSalary(id);

            //Assert
            //Assert.AreEqual(1000, estimate.AnnualCost);
            //Assert.AreEqual(1000, estimate2.AnnualCost);
        }

        private void AddEmployee(string id)
        {
            // Act
            var cmd = new Commands.AddEmployeeToBenefitsEstimate(id, "Mark", "Ewer", MaritalStatus.Single);
            var result = _processor.ProcessCommand(cmd);

            // Assert
            Assert.True(result.EventsWereEmitted);
        }

        private void SetSalary(string id)
        {
            // Act
            var cmd = new Commands.SetEmployeeSalary(id, 26 * 2000, 26);
            var result = _processor.ProcessCommand(cmd);

            // Assert
            Assert.True(result.EventsWereEmitted);
        }
    }
}
