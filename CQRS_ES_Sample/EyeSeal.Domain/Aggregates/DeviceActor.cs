using Akka.Persistence;
using EyeSeal.Domain.Messages.BreachDetectorSchema;
using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;
using EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EyeSeal.Domain.Aggregates
{
    public class DeviceActor : ReceivePersistentActor
    {
        #region Setup
        public override string PersistenceId { get; }
        private int _eventsSinceLastSnapshot = 0;
        DeviceState Entity { get; set; }

        public DeviceActor(string id)
        {
            if (string.IsNullOrEmpty(id)) id = $"{nameof(DeviceActor)}-{Guid.NewGuid().ToString("N")}";
            this.PersistenceId = id;
            this.SetupCommands();
            this.SetupEvents();
            Console.WriteLine($"{this.PersistenceId} -- COMMAND: ActorCreated");
        }
        void SetupCommands()
        {
            Command<Commands.ActivateDevice>(cmd => {
                Console.WriteLine($"{this.PersistenceId} -- COMMAND: ActivateDevice");
                var evt = new Events.DeviceActivated(cmd.IMEI, cmd.ICCID, cmd.EncryptionKey, cmd.EncryptionSalt);
                Persist(evt, e =>
                {
                    if(this.Entity==null) this.Entity = new DeviceState();
                    this.Entity.Apply(e);
                    this.CheckForSnapshot();
                    Context.System.EventStream.Publish(e);
                });
            });
            Command<Commands.DeployDevice>(cmd =>
            {
                Console.WriteLine($"{this.PersistenceId} -- COMMAND: DeployDevice");
                var evt = new Events.DeviceDeployed(this.Entity.IMEI, cmd.DeviceDeploymentDate, cmd.ShipperId, cmd.Consignee, cmd.ShippingLine, cmd.VesselName, cmd.VoyageNumber, cmd.ContainerNumber, cmd.SecuritySealNumber, cmd.OriginPort, cmd.DestinationPort, cmd.EstimatedDeparture, cmd.EstimateArrival);
                Persist(evt, e =>
                {
                    this.Entity.Apply(e);
                    this.CheckForSnapshot();
                    Context.System.EventStream.Publish(e);
                });
            });
            Command<Commands.RetireDevice>(cmd =>
            {
                Console.WriteLine($"{this.PersistenceId} -- COMMAND: RetireDevice");
                var evt = new Events.DeviceDeactivated(this.Entity.IMEI, cmd.RetiredOnDate);
                Persist(evt, e =>
                {
                    this.Entity.Apply(e);
                    this.CheckForSnapshot();
                    Context.System.EventStream.Publish(e);
                });
            });
            Command<Commands.RecordMessage>(cmd => this.ParseDeviceMessage(cmd));
            Command<SaveSnapshotSuccess>(success => {
                // handle snapshot save success...
                // DeleteMessages(success.Metadata.SequenceNr);
                this.DeleteSnapshots(new SnapshotSelectionCriteria(success.Metadata.SequenceNr - 50));
                _eventsSinceLastSnapshot = 0;
            });

            Command<SaveSnapshotFailure>(failure => {
                // handle snapshot save failure...                
            });
        }
        void SetupEvents()
        {
            Recover<Events.DeviceActivated>(evt =>
            {
                if (this.Entity == null) this.Entity = new DeviceState();
                this.Entity.Apply(evt);
                _eventsSinceLastSnapshot++;
            });
            Recover<Events.DeviceDeployed>(evt =>
            {
                this.Entity.Apply(evt);
                _eventsSinceLastSnapshot++;
            });
            Recover<Events.DeviceDeactivated>(evt =>
            {
                this.Entity.Apply(evt);
                _eventsSinceLastSnapshot++;
            });
            Recover<Events.MessageReceived>(evt =>
            {
                this.Entity.Apply(evt);
                _eventsSinceLastSnapshot++;
            });
            Recover<Events.AlertReceveived>(evt =>
            {
                evt.ToString();
                _eventsSinceLastSnapshot++;
            });
            Recover<SnapshotOffer>(snap =>
            {
                this.Entity = JsonConvert.DeserializeObject<DeviceState>(snap.Snapshot.ToString(), new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
            });
        }
        void CheckForSnapshot()
        {
            if (_eventsSinceLastSnapshot > 10)
            {
                SaveSnapshot(JsonConvert.SerializeObject(this.Entity));
            }
            else
            {
                _eventsSinceLastSnapshot++;
            }
        }

        void ParseDeviceMessage(Commands.RecordMessage cmd)
        {
            Console.WriteLine($"{this.PersistenceId} -- COMMAND: RecordMessage");
            var messageEvent = new Events.MessageReceived(cmd.IMEI, cmd.JSON);
            Persist(messageEvent, evt =>
            {
                this.Entity.Apply(evt);
                Context.System.EventStream.Publish(messageEvent);
                CheckForSnapshot();
            });

            var bdd = BreachDetectorData.FromJson(cmd.JSON);
            foreach(var l in bdd.Log)
            {
                if(l.Supdat.IsAlert())
                {
                    Persist(new Events.AlertReceveived(this.Entity.IMEI, l.ToJson()), evt =>
                    {
                        Console.WriteLine($"{this.PersistenceId} -- *ALERT*: {l.Errdesc}");
                        Context.System.EventStream.Publish(evt);
                        CheckForSnapshot();
                    });
                }
            }

        }
        #endregion Setup

        #region Entities
        public enum DeviceStatus { Testing, Active, Deactivated };
        public class DeviceState
        {
            public DeviceState() { }

            public DeviceStatus Status { get; private set; }
            public string IMEI { get; private set; }
            public string ICCID { get; private set; }
            public string EncryptionKey  { get; private set; }
            public string EncryptionSalt { get; private set; }

            public DateTime DeviceDeploymentDate  { get; private set; }
            public string ShipperId { get; private set; }
            public string Consignee { get; private set; }
            public string ShippingLine { get; private set; }
            public string VesselName { get; private set; }
            public string VoyageNumber { get; private set; }
            public string ContainerNumber { get; private set; }
            public string SecuritySealNumber { get; private set; }
            public string OriginPort { get; private set; }
            public string DestinationPort { get; private set; }
            public DateTime EstimatedDeparture { get; private set; }
            public DateTime EstimateArrival { get; private set; }
            public DateTime RetiredDate { get; private set; }

            public List<BreachDetectorData> Messages { get; private set;}

            public void Apply(Events.DeviceActivated evt)
            {
                this.Status = DeviceStatus.Testing;
                this.ICCID = evt.ICCID;
                this.IMEI = evt.IMEI;
                this.EncryptionKey = evt.EncryptionKey;
                this.EncryptionSalt = evt.EncryptionSalt;
                this.Messages = new List<BreachDetectorData>();
            }
            public void Apply(Events.DeviceDeployed evt)
            {
                Status = DeviceStatus.Active;
                this.DeviceDeploymentDate = evt.DeviceDeploymentDate;
                this.ShipperId = evt.ShipperId;
                this.Consignee = evt.Consignee;
                this.ShippingLine = evt.ShippingLine;
                this.VesselName = evt.VesselName;
                this.VoyageNumber = evt.VoyageNumber;
                this.ContainerNumber = evt.ContainerNumber;
                this.SecuritySealNumber = evt.SecuritySealNumber;
                this.OriginPort = evt.OriginPort;
                this.DestinationPort = evt.DestinationPort;
                this.EstimatedDeparture = evt.EstimatedDeparture;
                this.EstimateArrival = evt.EstimateArrival;
            }
            public void Apply(Events.DeviceDeactivated evt)
            {
                this.RetiredDate = evt.RetiredOnDate;
                Status = DeviceStatus.Deactivated;
            }
            public void Apply(Events.MessageReceived evt)
            {
                if(this.Messages==null) this.Messages = new List<BreachDetectorData>();
                var bdd = evt.GetMessage();
                this.Messages.Add(bdd);
            }
        }
        #endregion Entities

        public class Commands
        {
            public class ActivateDevice
            {
                public string IMEI { get; private set; }
                public string ICCID { get; private set; }
                public string EncryptionKey { get; private set; }
                public string EncryptionSalt { get; private set; }

                public ActivateDevice(string imei, string iccid, string key, string salt)
                {
                    this.ICCID = iccid;
                    this.IMEI = imei;
                    this.EncryptionKey = key;
                    this.EncryptionSalt = salt;
                }
            }
            public class DeployDevice
            {
                public DeployDevice(
                    DateTime deploymentDate,
                    string shipper,
                    string consignee,
                    string shipplingLine,
                    string vesselName,
                    string voyageNumber,
                    string containerNumber,
                    string securitySealNumber,
                    string originPort,
                    string destinationPort,
                    DateTime etd,
                    DateTime eta
                    )
                {
                    this.DeviceDeploymentDate = deploymentDate;
                    this.ShipperId = shipper;
                    this.Consignee = consignee;
                    this.ShippingLine = shipplingLine;
                    this.VesselName = vesselName;
                    this.VoyageNumber = voyageNumber;
                    this.ContainerNumber = containerNumber;
                    this.SecuritySealNumber = securitySealNumber;
                    this.OriginPort = originPort;
                    this.DestinationPort = destinationPort;
                    this.EstimatedDeparture = etd;
                    this.EstimateArrival = eta;
                }

                public DateTime DeviceDeploymentDate { get; private set; }
                public string ShipperId { get; private set; }
                public string Consignee { get; private set; }
                public string ShippingLine { get; private set; }
                public string VesselName { get; private set; }
                public string VoyageNumber { get; private set; }
                public string ContainerNumber { get; private set; }
                public string SecuritySealNumber { get; private set; }
                public string OriginPort { get; private set; }
                public string DestinationPort { get; private set; }
                public DateTime EstimatedDeparture { get; private set; }
                public DateTime EstimateArrival { get; private set; }
            }
            public class RetireDevice
            {
                public RetireDevice(DateTime retireDate) { this.RetiredOnDate = retireDate; }
                public DateTime RetiredOnDate { get; private set; }
            }
            public class RecordMessage
            {
                public string IMEI { get; private set; }
                public string JSON { get; private set; }
                public RecordMessage(string imei, string json)
                {
                    this.IMEI = imei;
                    this.JSON = json;
                }

                public BreachDetectorData GetMessage()
                {
                    return BreachDetectorData.FromJson(this.JSON);
                }
            }
        }

        public class Events
        {
            public class DeviceActivated : Commands.ActivateDevice
            {
                public DeviceActivated(string imei, string iccid, string key, string salt):base(imei, iccid, key, salt){ }
            }
            public class DeviceDeployed : Commands.DeployDevice
            {
                public string IMEI { get; private set; }
                public DeviceDeployed(string imei, DateTime deploymentDate, string shipper, string consignee, string shipplingLine, string vesselName, string voyageNumber, string containerNumber, string securitySealNumber, string originPort, string destinationPort, DateTime etd, DateTime eta) : base(deploymentDate, shipper, consignee, shipplingLine, vesselName, voyageNumber, containerNumber, securitySealNumber, originPort, destinationPort, etd, eta)
                {
                    this.IMEI = imei;
                }
            }
            public class DeviceDeactivated : Commands.RetireDevice
            {
                public string IMEI { get; private set; }
                public DeviceDeactivated(string imei, DateTime retireDate) : base(retireDate) { this.IMEI = imei; }
            }
            public class MessageReceived : Commands.RecordMessage
            {
                public MessageReceived(string imei, string json) : base(imei, json) { }
            }
            public class AlertReceveived
            {
                public string IMEI { get; private set; }
                public string JSON { get; private set; }
                public AlertReceveived(string imei, string json)
                {
                    this.IMEI = imei;
                    this.JSON = json;
                }
                public LogSchema GetAlert()
                {
                    return LogSchema.FromJson(this.JSON);
                }
            }
        }
    }
}
