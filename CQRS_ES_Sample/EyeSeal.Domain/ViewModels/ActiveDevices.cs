using Akka.Actor;
using EyeSeal.Domain.Aggregates;
using EyeSeal.Domain.Messages;
using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;
using EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeSeal.Domain.ViewModels
{
    public class ActiveDevices : ReceiveActor
    {
        Dictionary<string, ActiveDevice> _devices { get; set; }
        string _file = Path.Combine("ViewModels", $"{nameof(DevicesBeingTested)}.json");
        bool _isSaved = true;
        public ActiveDevices()
        {
            _devices = new Dictionary<string, ActiveDevice>();
            Receive<DeviceActor.Events.DeviceActivated>(evt => Remove(evt.IMEI));
            Receive<DeviceActor.Events.DeviceDeployed>(evt => Upsert(evt));
            Receive<DeviceActor.Events.DeviceDeactivated>(evt => Remove(evt.IMEI));
            Receive<DeviceActor.Events.MessageReceived>(evt => ReceiveMessage(evt));
            Receive<DeviceActor.Events.AlertReceveived>(evt => SetAlert(evt));

            Receive<SaveCommand>(cmd => Save());
            Receive<PrintState>(cmd => ShowStateOnConsole());
            Receive<ShowAlerts>(cmd => ShowAlertsOnConsole());
        }
        private void Upsert(DeviceActor.Events.DeviceDeployed evt)
        {
            ActiveDevice d;
            if(_devices.ContainsKey(evt.IMEI))
            {
                d = _devices[evt.IMEI];
            }
            else
            {
                d = new ActiveDevice();
                _devices.Add(evt.IMEI, d);
            }
            d.IMEI = evt.IMEI;
            d.ShipperId = evt.ShipperId;
            d.Consignee = evt.Consignee;
            d.ContainerNumber=evt.ContainerNumber;
            d.SecuritySealNumber=evt.SecuritySealNumber;
            d.ShippingLine=evt.ShippingLine;
            d.VesselName=evt.VesselName;
            d.VoyageNumber=evt.VoyageNumber;
            d.OriginPort=evt.OriginPort;
            d.DestinationPort=evt.DestinationPort;
            d.EstimatedDeparture=evt.EstimatedDeparture;
            d.EstimateArrival=evt.EstimateArrival;
            d.DeviceDeploymentDate = evt.DeviceDeploymentDate;
            _isSaved = false;
            Self.Tell(new SaveCommand());
        }
        private void Remove(string imei)
        {
            if (_devices.ContainsKey(imei))
            {
                _devices.Remove(imei);
                _isSaved = false;
                Self.Tell(new SaveCommand());
            }
        }
        private void ReceiveMessage(DeviceActor.Events.MessageReceived evt)
        {
            if (_devices.ContainsKey(evt.IMEI))
            {
                var m = evt.GetMessage();
                _devices[evt.IMEI].Location.Mcc = m.Location.Mcc;
                _devices[evt.IMEI].Location.Cid = m.Location.Cid;
                _devices[evt.IMEI].Location.Lac = m.Location.Lac;
                _devices[evt.IMEI].Location.Mnc = m.Location.Mnc;
                _isSaved = false;
                Self.Tell(new SaveCommand());
            }
        }
        private void SetAlert(DeviceActor.Events.AlertReceveived evt)
        {
            if(_devices.ContainsKey(evt.IMEI))
            {
                _devices[evt.IMEI].Alert = evt.GetAlert().Supdat;
                _isSaved = false;
                Self.Tell(new SaveCommand());
            }
        }
        private void ShowStateOnConsole()
        {
            var s = new StringBuilder();

            s.AppendLine();
            s.AppendLine($"----------------------------------------------------------------");
            s.AppendLine($"----------         Actively Deployed Devices          ----------");
            s.AppendLine($"- {"IMEI",15}{"Consignee",15}{"Departure",15}{"Arrival",15} -");
            foreach (var d in _devices.Values)
                s.AppendLine($"- {d.IMEI, 15}{d.Consignee, 15}{d.EstimatedDeparture.ToShortDateString(), 15}{d.EstimateArrival.ToShortDateString(),15} -");
            s.AppendLine($"----------------------------------------------------------------");

            Console.Write(s.ToString());
        }
        private void ShowAlertsOnConsole()
        {
            var s = new StringBuilder();

            s.AppendLine();
            s.AppendLine($"----------------------------------------------------------------");
            s.AppendLine($"----------      Device Has Current Breach Event       ----------");
            s.AppendLine($"- {"IMEI",15}{"Alert",20}{"Description",25} -");
            foreach (var d in _devices.Values)
            {
                if (d.Alert.GetType() == typeof(LightEvent))
                {
                    var levt = d.Alert as LightEvent;
                    s.AppendLine($"- {d.IMEI,15}{levt.EventType.ToString(),20}{levt.Lights.Event,25} -");
                }
                else if (d.Alert.GetType() == typeof(DoorEvent))
                {
                    var devt = d.Alert as DoorEvent;
                    s.AppendLine($"- {d.IMEI,15}{devt.EventType.ToString(),20}{devt.Doors.Event,25} -");
                }
            }
            s.AppendLine($"----------------------------------------------------------------");

            Console.Write(s.ToString());
        }
        private void Save()
        {
            if (!_isSaved)
            {
                var json = JsonConvert.SerializeObject(this._devices);
                System.IO.File.WriteAllText(_file, json);
                _isSaved = true;
            }
        }
        private void Load()
        {
            if (!Directory.Exists("ViewModels")) Directory.CreateDirectory("ViewModels");
            if (File.Exists(_file))
            {
                var json = File.ReadAllText(_file);
                this._devices = JsonConvert.DeserializeObject<Dictionary<string, ActiveDevice>>(json, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
            }
        }

        void RegisterSubscriptions()
        {
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.DeviceActivated));
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.DeviceDeployed));
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.MessageReceived));
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.AlertReceveived));
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.DeviceDeactivated));
        }
        public override void AroundPreStart()
        {
            RegisterSubscriptions();
            Load();
            base.AroundPreStart();
        }
        public override void AroundPreRestart(Exception cause, object message)
        {
            Load();
            base.AroundPreRestart(cause, message);
        }
        public override void AroundPostStop()
        {
            Self.Tell(new SaveCommand());
            base.AroundPostStop();
        }

        public class ActiveDevice
        {
            public ActiveDevice() { this.Location = new CellTowerLocation(); }
            public EventBase Alert { get; set; }
            public CellTowerLocation Location { get; set; }
            public string IMEI { get;  set; }
            public DateTime DeviceDeploymentDate { get;  set; }
            public string ShipperId { get;  set; }
            public string Consignee { get;  set; }
            public string ShippingLine { get;  set; }
            public string VesselName { get;  set; }
            public string VoyageNumber { get;  set; }
            public string ContainerNumber { get;  set; }
            public string SecuritySealNumber { get;  set; }
            public string OriginPort { get;  set; }
            public string DestinationPort { get;  set; }
            public DateTime EstimatedDeparture { get;  set; }
            public DateTime EstimateArrival { get;  set; }
        }
        public class CellTowerLocation
        {
            public int Mcc { get; set; } = 0;
            public int Mnc { get; set; } = 0;
            public int Lac { get; set; } = 0;
            public int Cid { get; set; } = 0;
        }
    }
}
