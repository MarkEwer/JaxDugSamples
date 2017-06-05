using Server.Events.Franchise;
using Server.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Franchise
    {
        public Franchise() { }

        public string ID { get; protected set; }
        public string Name { get; protected set; }
        public Person Owner { get; protected set; }
        public StreetAddress MailingAddress { get; protected set; }
        public DateTime DateOfFormation { get; protected set; }
        public DateTime DateClosed { get; protected set; }
        public bool AgreementSigned { get; protected set; }
        public string TaxId { get; protected set; }

        public void Apply(EntityId @event)
        {
            this.ID = @event.Id;
            this.AgreementSigned = false;
        }
        public void Apply(FranchiseRegistered @event)
        {
            this.Name = @event.Name;
            this.Owner = new Person(@event.GivenName, @event.Surname);
            this.MailingAddress = @event.BillingAddress;
        }
        public void Apply(FranchiseMoved @event)
        {
            this.MailingAddress = @event.NewMailingAddress;
        }
        public void Apply(FranchiseTaxIdRegistered @event)
        {
            this.TaxId = @event.TaxId;
        }
        public void Apply(FranchiseAgreementSigned @event)
        {
            this.AgreementSigned = true;
            this.DateOfFormation = @event.SignedOnDate;
        }
        public void Apply(FranchiseSold @event)
        {
            this.Name = @event.NewCorporateName;
            this.Owner = new Person(@event.NewOwnerGiveName, @event.NewOwnerSurname);
            this.TaxId = string.Empty;
        }
        public void Apply(FranchiseClosed @event)
        {
            this.DateClosed = @event.ClosedOn;
        }
    }
}
