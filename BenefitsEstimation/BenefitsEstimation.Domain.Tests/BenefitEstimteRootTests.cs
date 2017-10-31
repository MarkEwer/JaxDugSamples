using Benefits.Domain.Models;
using Benefits.Domain.ViewModels;
using d60.Cirqus;
using d60.Cirqus.Events;
using d60.Cirqus.Ntfs.Config;
using d60.Cirqus.Testing.Internals;
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
        private static readonly TimeSpan TIMEOUT = new TimeSpan(0, 0, 5);

        private ICommandProcessor _processor;
        private IEventStore _eventstore;
        private InMemoryViewManager<BenefitEstimateViewModel> _view;

        [OneTimeSetUp] public void StartDomain()
        {
            _view = new InMemoryViewManager<BenefitEstimateViewModel>();
            _eventstore = new InMemoryEventStore();

            _processor = CommandProcessor.With()
                .EventStore(c => c.RegisterInstance(_eventstore))
                .EventDispatcher(e => e.UseViewManagerEventDispatcher(new IViewManager[] { _view }))
                .Create();
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

        [Test]
        public void ValidateQuoteWhenWeAddThemAllThenRemoveThemAll()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");

            //Act
            AddEmployee(id);
            SetSalary(id);
            AddSpouse(id, "Heather", "Ewer");
            AddDependent(id, "Nora", "Ewer");
            AddDependent(id, "Matthew", "Ewer");
            AddDependent(id, "Seth", "Ewer");
            AddDependent(id, "Rose", "Ewer");
            RemoveSpouse(id, "Heather", "Ewer");
            RemoveDependent(id, "Nora", "Ewer");
            RemoveDependent(id, "Matthew", "Ewer");
            RemoveDependent(id, "Seth", "Ewer");
            RemoveDependent(id, "Rose", "Ewer");

            System.Threading.Thread.Sleep(1000); //Wait for view to catch up.
            var v = _view.Load(id);

            //Assert
            Assert.AreEqual("Mark", v.Employee.Value.FirstName);
            Assert.AreEqual(26 * 2000, v.Salary);
            Assert.IsFalse(v.Spouse.HasValue);
            Assert.AreEqual(Math.Round(1000d / 26d, 2), v.DeductionPerPaycheck);
            Assert.AreEqual(0, v.Dependents.Count());
        }

        [Test]
        public void ValidateQuoteForEmployeeWithSpouseAndTwoMinusOneChildren()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");

            //Act
            AddEmployee(id);
            SetSalary(id);
            AddSpouse(id, "Heather", "Ewer");
            AddDependent(id, "Nora", "Ewer");
            AddDependent(id, "Matthew", "Ewer");
            RemoveDependent(id, "Matthew", "Ewer");

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
        [Test]
        public void ValidateQuoteForEmployeeWithSpouseAndOneChildAndAllHaveDiscounts()
        {
            // Arrange
            var id = Guid.NewGuid().ToString("N");

            //Act
            AddEmployee(id);
            SetSalary(id);
            AddSpouse(id, "Alison", "Ewer");
            AddDependent(id, "Alex", "Ewer");

            System.Threading.Thread.Sleep(1000); //Wait for view to catch up.
            var v = _view.Load(id);

            //Assert
            Assert.AreEqual("Mark", v.Employee.Value.FirstName);
            Assert.AreEqual(26 * 2000, v.Salary);
            Assert.AreEqual("Alison", v.Spouse.Value.FirstName);
            Assert.AreEqual(1, v.Dependents.Count());

            // (1000 for employee) + (450 for spouse) + (450 for child) = 1900 per year
            Assert.AreEqual(Math.Round(1900d / 26d, 2), v.DeductionPerPaycheck);
        }

        private void AddEmployee(string id)
        {
            // Act
            var cmd = new Commands.AddEmployeeToBenefitsEstimate(
                id, "Mark", "Ewer");
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

        private void RemoveSpouse(string id, string firstName, string lastName)
        {
            // Act
            var cmd = new Commands.RemoveSpouseToBenefitsEstimate(id, firstName, lastName);
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

        private void RemoveDependent(string id, string firstName, string lastName)
        {
            // Act
            var cmd = new Commands.RemoveDependentToBenefitsEstimate(id, firstName, lastName);
            var result = _processor.ProcessCommand(cmd);

            // Assert
            Assert.True(result.EventsWereEmitted);
            _view.WaitUntilProcessed(result, TIMEOUT);
        }
    }
}
