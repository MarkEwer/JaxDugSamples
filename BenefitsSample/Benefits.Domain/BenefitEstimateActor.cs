using Akka.Actor;
using Akka.Persistence;
using Benefits.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain
{
    public class BenefitEstimateActor : ReceiveActor 
    //public class BenefitEstimateActor: ReceivePersistentActor
    {
        private List<Func<Models.Estimate, Models.Estimate>> _rules;
        protected string Id { get; set; }
        private Models.BenfitEstimate Model { get; }
        //public override string PersistenceId => $"Estimate-{Id}";
        public string PersistenceId => $"Estimate-{Id}";

        public BenefitEstimateActor(string id):base()
        {
            this.Id = id;
            this.InitializeCommandHandlers();
            this.InitializeEventRecovery();
            this.Model = new Models.BenfitEstimate(id);
            this._rules = new List<Func<Models.Estimate, Models.Estimate>>();
        }

        private void InitializeCommandHandlers()
        {
            this.Receive<Commands.AddEmployeeToBenefitsEstimate>(cmd => 
                this.AddEmployee(cmd)
                );
            this.Receive<Commands.SetEmployeeSalary>(cmd => this.SetSalary(cmd));
            this.Receive<Commands.AddSpouseToBenefitsEstimate>(cmd => this.AddSpouse(cmd));
            this.Receive<Commands.AddDependentToBenefitsEstimate>(cmd => this.AddDependent(cmd));
            this.Receive<Commands.RemoveSpouseToBenefitsEstimate>(cmd => this.RemoveSpouse(cmd));
            this.Receive<Commands.RemoveDependentToBenefitsEstimate>(cmd => this.RemoveDependent(cmd));
            this.Receive<string>(x => 
                System.Diagnostics.Debug.WriteLine(x)
                );
            this.ReceiveAny(cmd =>
            {
                Sender.Tell(OperationResult.Failure(this.Id), Self);
            });

            //this.Command<Commands.AddEmployeeToBenefitsEstimate>(cmd =>
            //    this.AddEmployee(cmd)
            //    );
            //this.Command<Commands.SetEmployeeSalary>(cmd => this.SetSalary(cmd));
            //this.Command<Commands.AddSpouseToBenefitsEstimate>(cmd => this.AddSpouse(cmd));
            //this.Command<Commands.AddDependentToBenefitsEstimate>(cmd => this.AddDependent(cmd));
            //this.Command<Commands.RemoveSpouseToBenefitsEstimate>(cmd => this.RemoveSpouse(cmd));
            //this.Command<Commands.RemoveDependentToBenefitsEstimate>(cmd => this.RemoveDependent(cmd));
            //this.Command<string>(x =>
            //    System.Diagnostics.Debug.WriteLine(x)
            //    );
            //this.CommandAny(cmd =>
            //{
            //    Sender.Tell(OperationResult.Failure(this.Id), Self);
            //});
        }

        private void InitializeEventRecovery()
        {
            //this.Recover<Events.EstimateCreated>(evt => this.Model.Apply(evt));
            //this.Recover<Events.SalarySpecified>(evt => this.Model.Apply(evt));
            //this.Recover<Events.SpouseAdded>(evt => this.Model.Apply(evt));
            //this.Recover<Events.DependentAdded>(evt => this.Model.Apply(evt));
            //this.Recover<Events.SpouseRemoved>(evt => this.Model.Apply(evt));
            //this.Recover<Events.DependentRemoved>(evt => this.Model.Apply(evt));
        }

        // Command Handlers
        private void AddEmployee(Commands.AddEmployeeToBenefitsEstimate cmd)
        {
            if (this.Model.Employee.HasValue)
            {
                Sender.Tell(OperationResult.Failure(this.PersistenceId), Self);
            }
            else
            {
                var evt = new EstimateCreated(this.PersistenceId, cmd.FirstName, cmd.LastName, cmd.MaritalStatus);
                //this.Persist(created, evt =>
                //{
                    this.Model.Apply(evt);
                    Context.System.EventStream.Publish(evt);
                    var estimate = this.Model.BroadcastEstimate();
                    if (estimate != null) Context.System.EventStream.Publish(estimate);
                    Sender.Tell(OperationResult.Success(this.PersistenceId), Self);
                //});
            }
        }

        private void SetSalary(Commands.SetEmployeeSalary cmd)
        {
            if (this.Model.Employee.HasValue)
            {
                var evt = new Events.SalarySpecified(this.PersistenceId, cmd.AnnualSalary, cmd.NumberOfPaychecksPerYear);
                //this.Persist(salary, evt =>
                //{
                    this.Model.Apply(evt);
                    Context.System.EventStream.Publish(evt);
                    var estimate = this.Model.BroadcastEstimate();
                    if (estimate != null) Context.System.EventStream.Publish(estimate);
                    Sender.Tell(OperationResult.Success(this.PersistenceId), Self);
                //});
            }
            else
            {
                Sender.Tell(OperationResult.Failure(this.PersistenceId), Self);
            }
        }

        private void AddSpouse(Commands.AddSpouseToBenefitsEstimate cmd)
        {
            if(this.Model.Spouse.HasValue)
            {
                this.Sender.Tell(OperationResult.Failure(this.Id), this.Self);
            }
            else
            {
                var evt = new Events.SpouseAdded(this.Id, cmd.FirstName, cmd.LastName);
                //this.Persist(evt, x =>
                //{
                    this.Model.Apply(evt);
                    var estimate = this.Model.BroadcastEstimate();
                    if (estimate != null) Context.System.EventStream.Publish(estimate);
                    Sender.Tell(OperationResult.Success(this.PersistenceId), Self);
                //});
            }
        }

        private void AddDependent(Commands.AddDependentToBenefitsEstimate cmd)
        {
            if (this.Model.Dependents.Any(x => x.FirstName.Equals(cmd.FirstName, StringComparison.OrdinalIgnoreCase)
                                            && x.LastName.Equals(cmd.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                this.Sender.Tell(OperationResult.Failure(this.Id), this.Self);
            }
            else
            {
                var evt = new Events.DependentAdded(this.Id, cmd.FirstName, cmd.LastName);
                //this.Persist(evt, x =>
                //{
                    this.Model.Apply(evt);
                    var estimate = this.Model.BroadcastEstimate();
                    if (estimate != null) Context.System.EventStream.Publish(estimate);
                    Sender.Tell(OperationResult.Success(this.PersistenceId), Self);
                //});
            }
        }

        private void RemoveSpouse(Commands.RemoveSpouseToBenefitsEstimate cmd)
        {
            if(this.Model.Spouse.HasValue
                && this.Model.Spouse.Value.FirstName.Equals(cmd.FirstName, StringComparison.OrdinalIgnoreCase)
                && this.Model.Spouse.Value.LastName.Equals(cmd.LastName, StringComparison.OrdinalIgnoreCase)
                )
            {
                var evt = new Events.SpouseAdded(this.Id, cmd.FirstName, cmd.LastName);
                //this.Persist(evt, x =>
                //{
                    this.Model.Apply(evt);
                    var estimate = this.Model.BroadcastEstimate();
                    if (estimate != null) Context.System.EventStream.Publish(estimate);
                    Sender.Tell(OperationResult.Success(this.PersistenceId), Self);
                //});
            }
            else
            {
                Sender.Tell(OperationResult.Failure(this.Id), Self);
            }
        }

        private void RemoveDependent(Commands.RemoveDependentToBenefitsEstimate cmd)
        {
            if (this.Model.Dependents.Any(x => x.FirstName.Equals(cmd.FirstName, StringComparison.OrdinalIgnoreCase)
                                            && x.LastName.Equals(cmd.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                var evt = new Events.DependentRemoved(this.Id, cmd.FirstName, cmd.LastName);
                //this.Persist(evt, x =>
                //{
                    this.Model.Apply(evt);
                    var estimate = this.Model.BroadcastEstimate();
                    if(estimate != null) Context.System.EventStream.Publish(estimate);
                    Sender.Tell(OperationResult.Success(this.PersistenceId), Self);
                //});
            }
            else
            {
                this.Sender.Tell(OperationResult.Failure(this.Id), this.Self);
            }
        }
    }
}
