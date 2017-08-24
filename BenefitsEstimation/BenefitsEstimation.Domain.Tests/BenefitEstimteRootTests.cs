using Benefits.Domain.Models;
using Benefits.Domain.ViewModels;
using d60.Cirqus;
using d60.Cirqus.Ntfs.Config;
using d60.Cirqus.Views;
using d60.Cirqus.Views.ViewManagers;
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
        private static readonly string EVENTS_FOLDER = "UnitTestEvents";
        private static readonly TimeSpan TIMEOUT = new TimeSpan(0, 0, 5);

        private ICommandProcessor _processor;
        private List<IViewManager> _viewManagers;
        private InMemoryViewManager<BenefitEstimateViewModel> _view = new InMemoryViewManager<BenefitEstimateViewModel>();

        [OneTimeSetUp] public void StartDomain()
        {
            var path = this.GetType().Assembly.Location;
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = Path.Combine(path, EVENTS_FOLDER);

            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            _processor = CommandProcessor.With()
                .EventStore(c => c.UseFiles(path))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(GetViewManagers().ToArray()))
                .Create();
        }
        public List<IViewManager> GetViewManagers()
        {
            _viewManagers = new List<IViewManager> { _view };
            return _viewManagers;
        }
        [OneTimeTearDown] public void StopDomain()
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

            System.Threading.Thread.Sleep(1000); //Wait for view to catch up.
            var v = _view.Load(id);

            //Assert
            Assert.AreEqual("Mark", v.Employee.Value.FirstName);
            Assert.AreEqual(26 * 2000, v.Salary);
            Assert.IsFalse(v.Spouse.HasValue);
            Assert.AreEqual(Math.Round(1000d/26d, 2), v.DeductionPerPaycheck);
        }

        [Test]
        public void ValidateQuoteForEmployeeWithFourDependentsAndNoSpouse()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");

            //Act
            AddEmployee(id);
            SetSalary(id);
            AddDependent(id, "Nora", "Ewer");
            AddDependent(id, "Matthew", "Ewer");
            AddDependent(id, "Seth", "Ewer");
            AddDependent(id, "Rose", "Ewer");

            System.Threading.Thread.Sleep(1000); //Wait for view to catch up.
            var v = _view.Load(id);

            //Assert
            Assert.AreEqual("Mark", v.Employee.Value.FirstName);
            Assert.AreEqual(26 * 2000, v.Salary);
            Assert.IsFalse(v.Spouse.HasValue);
            Assert.AreEqual(4, v.Dependents.Count());

            // (1000 for employee) + (500 * 4 children) = 3000 per year
            Assert.AreEqual(Math.Round(3000d / 26d, 2), v.DeductionPerPaycheck);
        }

        [Test]
        public void ValidateQuoteForEmployeeWithSpouseAndOneChild()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");

            //Act
            AddEmployee(id);
            SetSalary(id);
            AddSpouse(id, "Heather", "Ewer");
            AddDependent(id, "Nora", "Ewer");

            System.Threading.Thread.Sleep(1000); //Wait for view to catch up.
            var v = _view.Load(id);

            //Assert
            Assert.AreEqual("Mark", v.Employee.Value.FirstName);
            Assert.AreEqual(26 * 2000, v.Salary);
            Assert.AreEqual("Heather", v.Spouse.Value.FirstName);
            Assert.AreEqual(1, v.Dependents.Count());

            // (1000 for employee) + (500 for spouse) + (500 for child) = 2000 per year
            Assert.AreEqual(Math.Round(2000d / 26d, 2), v.DeductionPerPaycheck);
        }

        private void AddEmployee(string id)
        {
            // Act
            var cmd = new Commands.AddEmployeeToBenefitsEstimate(id, "Mark", "Ewer", MaritalStatus.Single);
            var result = _processor.ProcessCommand(cmd);

            // Assert
            Assert.True(result.EventsWereEmitted);
            _view.WaitUntilProcessed(result, TIMEOUT);
        }

        private void SetSalary(string id)
        {
            // Act
            var cmd = new Commands.SetEmployeeSalary(id, 26 * 2000, 26);
            var result = _processor.ProcessCommand(cmd);
            _view.WaitUntilProcessed(result, new TimeSpan(0, 0, 1));

            // Assert
            Assert.True(result.EventsWereEmitted);
            _view.WaitUntilProcessed(result, TIMEOUT);
        }

        private void AddSpouse(string id, string firstName, string lastName)
        {
            // Act
            var cmd = new Commands.AddSpouseToBenefitsEstimate(id, firstName, lastName);
            var result = _processor.ProcessCommand(cmd);

            // Assert
            Assert.True(result.EventsWereEmitted);
            _view.WaitUntilProcessed(result, TIMEOUT);
        }

        private void AddDependent(string id, string firstName, string lastName)
        {
            // Act
            var cmd = new Commands.AddDependentToBenefitsEstimate(id, firstName, lastName);
            var result = _processor.ProcessCommand(cmd);

            // Assert
            Assert.True(result.EventsWereEmitted);
            _view.WaitUntilProcessed(result, TIMEOUT);
        }
    }
}
