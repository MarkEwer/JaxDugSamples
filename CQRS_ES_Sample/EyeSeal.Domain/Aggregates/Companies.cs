using Akka.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeSeal.Domain.Aggregates
{
    public class CompanyActor : ReceivePersistentActor
    {
        #region Setup
        public override string PersistenceId { get; }
        CompanyState Entity { get; set; }

        public CompanyActor(string id)
        {
            if (string.IsNullOrEmpty(id)) id = $"{nameof(CompanyActor)}-{Guid.NewGuid().ToString("N")}";
            this.PersistenceId = id;
            this.SetupCommands();
            this.SetupEvents();
        }
        void SetupCommands()
        {
            Command<Commands.CreateNewCompany>(cmd => {
                var evt1 = new Events.CompanyCreated(this.PersistenceId);
                var evt2 = new Events.NameChanged(cmd.Name);
                Persist(evt1, evt =>
                {
                    this.Entity = new CompanyState(evt);
                    Context.System.EventStream.Publish(evt);
                    Persist(evt2, e =>
                    {
                        this.Entity.Apply(e);
                        this.CheckForSnapshot();
                        Context.System.EventStream.Publish(e);
                    });
                });
            });
            Command<Commands.ChangeCompanyName>(cmd =>
            {
                var evt = new Events.NameChanged(cmd.Name);
                Persist(evt, e =>
                {
                    this.Entity.Apply(e);
                    this.CheckForSnapshot();
                    Context.System.EventStream.Publish(e);
                });
            });
        }
        void SetupEvents()
        {
            Recover<Events.CompanyCreated>(evt => this.Entity = new CompanyState(evt) );
            Recover<Events.NameChanged>(evt =>this.Entity.Apply(evt));
            Recover<SnapshotOffer>(snap => this.Entity = JsonConvert.DeserializeObject<CompanyState>(snap.Snapshot.ToString()));
        }
        void CheckForSnapshot()
        {
            if (this.LastSequenceNr - this.SnapshotSequenceNr > 1000)
                SaveSnapshot(JsonConvert.SerializeObject(this.Entity));
        }
        #endregion Setup

        #region Entities
        public class CompanyState
        {
            public CompanyState(Events.CompanyCreated evt) { this.PersistenceId = evt.Id; }
            public string PersistenceId { get; private set; }
            public string Name { get; private set; }

            public void Apply(Events.NameChanged evt)
            {
                this.Name = evt.Name;
            }
        }
        #endregion Entities

        public class Commands
        {
            public class CreateNewCompany
            {
                public string Name { get; private set; }
                public CreateNewCompany(string name)
                {
                    this.Name = name;
                }
            }
            public class ChangeCompanyName : CreateNewCompany
            {
                public ChangeCompanyName(string name) : base(name) { }
            }
        }

        public class Events
        {
            public class CompanyCreated
            {
                public string Id { get; private set; }
                public CompanyCreated(string id)
                {
                    this.Id = id;
                }
            }
            public class NameChanged
            {
                public string Name { get; private set; }
                public NameChanged(string name)
                {
                    this.Name = name;
                }
            }

        }
    }
}
