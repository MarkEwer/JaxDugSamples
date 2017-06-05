using Akka.Actor;
using Server.Commands;
using Server.Commands.Franchise;
using Server.Events.Franchise;
using Server.Exceptions.Franchise;
using Server.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Actors
{
    public class FranchiseActor:ReceiveActor
    {
        #region Entity
        public string PersistenceId { get; }
        protected Entities.Franchise Entity { get; set; }
        #endregion

        #region Constructor
        public FranchiseActor() : this($"Franchise-{Guid.NewGuid().ToString("N")}") { }
        public FranchiseActor(string id)
        {
            this.PersistenceId = id;
            this.Become(InitialState);
        }
        public static Props Props(string id)
        {
            return Akka.Actor.Props.Create(() => new FranchiseActor(id));
        }
        #endregion

        #region States
        void InitialState()
        {
            this.Receive<RegisterInterestedFranchisee>(cmd => 
            {
                this.InitializeFranchiesRecord();
                this.UpdateFranchiseeInformation(
                    cmd.Name, cmd.GivenName, cmd.Surname, cmd.BillingAddress);
                this.Become(Inactive);
            });
            this.Receive<object>(@cmd => {
                throw new UnknownFranchiseException("Franchisee has not yet registered interest.");
            });
        }
        void Inactive()
        {
            this.Receive<ShareState>(cmd => 
            Sender.Tell(this.Entity)
            );
            this.Receive<MoveFranchise>(cmd => MoveFranchiseLocation(cmd.MovingToLocation) );
            this.Receive<RegisterFranchiseTaxId>(cmd => RegisterFranchiseTaxId(cmd.TaxId));
            this.Receive<RefuseFranchiseAgreement>(cmd =>
            {
                CloseFranchise(cmd.RefusedOn);
                this.Become(Closed);
            });
            this.Receive<SignFranchiseAgreement>(cmd => 
            {
                this.SignFranchiseAgreement(cmd.AgreementSigningDate);
                this.Become(Operational);
            });
            this.Receive<object>(cmd => {
                throw new InactiveFranchiseException("This operation is not allowed when the Franchise is not yet operational.");
            });
        }
        void Operational()
        {
            this.Receive<ShareState>(cmd => 
            Sender.Tell(this.Entity)
            );
            this.Receive<SellFranchise>(cmd =>
            {
                CloseFranchise(cmd.SellingDate);
                SellFranchise(cmd.SellingDate, cmd.NewCorporateName, cmd.NewOwnerGiveName, cmd.NewOwnerSurname);
                MoveFranchiseLocation(cmd.MovingToLocation);
                this.Become(Transfered);
            });
            this.Receive<TerminateFranchiseAgreement>(cmd =>
            {
                CloseFranchise(cmd.TerminationDate);
                this.Become(Closed);
            });
            this.Receive<object>(cmd => {
                throw new InvalidOperationException("This operation is not allowed when the Franchise is not yet operational.");
            });
        }
        void Closed()
        {
            this.Receive<ShareState>(cmd => 
            Sender.Tell(this.Entity)
            );
            this.Receive<MoveFranchise>(cmd => MoveFranchiseLocation(cmd.MovingToLocation));
            this.Receive<SignFranchiseAgreement>(cmd =>
            {
                this.SignFranchiseAgreement(cmd.AgreementSigningDate);
                this.Become(Operational);
            });
            this.Receive<SellFranchise>(cmd =>
            {
                SellFranchise(cmd.SellingDate, cmd.NewCorporateName, cmd.NewOwnerGiveName, cmd.NewOwnerSurname);
                MoveFranchiseLocation(cmd.MovingToLocation);
                this.Become(Transfered);
            });
            this.Receive<object>(cmd => {
                throw new InvalidOperationException("This operation is not allowed when the Franchise is closed.");
            });
        }
        void Transfered()
        {
            this.Receive<ShareState>(cmd => 
            Sender.Tell(this.Entity)
            );
            this.Receive<SellFranchise>(cmd =>
            {
                SellFranchise(cmd.SellingDate, cmd.NewCorporateName, cmd.NewOwnerGiveName, cmd.NewOwnerSurname);
                MoveFranchiseLocation(cmd.MovingToLocation);
            });
            this.Receive<MoveFranchise>(cmd => MoveFranchiseLocation(cmd.MovingToLocation));
            this.Receive<RegisterFranchiseTaxId>(cmd => RegisterFranchiseTaxId(cmd.TaxId));
            this.Receive<SignFranchiseAgreement>(cmd =>
            {
                this.SignFranchiseAgreement(cmd.AgreementSigningDate);
                this.Become(Operational);
            });
            this.Receive<object>(cmd => {
                throw new InactiveFranchiseException("This operation is not allowed when the Franchise is not operational.");
            });
        }
        #endregion

        #region Operations
        void InitializeFranchiesRecord()
        {
            this.Entity = new Entities.Franchise();
            var @event = new EntityId(PersistenceId);
            this.Entity.Apply(@event);
        }
        void UpdateFranchiseeInformation(string name, string givenName, string surname, StreetAddress address)
        {
            var @event = new FranchiseRegistered(name, givenName, surname, address);
            this.Entity.Apply(@event);
        }
        void MoveFranchiseLocation(StreetAddress location)
        {
            var @event = new FranchiseMoved(this.Entity.ID, location);
            this.Entity.Apply(@event);
        }
        void RegisterFranchiseTaxId(string taxId)
        {
            if(string.IsNullOrEmpty(this.Entity.TaxId))
            {
                var @event = new FranchiseTaxIdRegistered(this.Entity.ID, taxId);
                this.Entity.Apply(@event);
            }
            else
            {
                throw new InvalidOperationException("Cannot change a businesses TaxId without change of ownership first.");
            }
        }
        void SignFranchiseAgreement(DateTime signingDate)
        {
            if (string.IsNullOrEmpty(this.Entity.TaxId)) throw new InvalidOperationException("Cannot sign agreement without a tax id.");
            var @event = new FranchiseAgreementSigned(this.Entity.ID, signingDate);
            this.Entity.Apply(@event);
        }
        void CloseFranchise(DateTime closedOnDate)
        {
            var @event = new FranchiseClosed(this.Entity.ID, closedOnDate);
            this.Entity.Apply(@event);
        }
        void SellFranchise(DateTime soldOn, string name, string givenName, string surname)
        {
            var @event = new FranchiseSold(this.Entity.ID, soldOn, name, givenName, surname);
            this.Entity.Apply(@event);
        }
        #endregion Operations
    }
}
