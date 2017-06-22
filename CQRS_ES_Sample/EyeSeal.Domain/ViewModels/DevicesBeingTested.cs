using Akka.Actor;
using EyeSeal.Domain.Aggregates;
using EyeSeal.Domain.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EyeSeal.Domain.ViewModels
{
    public class DevicesBeingTested : ReceiveActor
    {
        Dictionary<string, TestDevice> _devices { get; set; }
        string _file = Path.Combine("ViewModels", $"{nameof(DevicesBeingTested)}.json");
        bool _isSaved = true;

        public DevicesBeingTested()
        {
            _devices = new Dictionary<string, TestDevice>();
            Receive<DeviceActor.Events.DeviceActivated>(evt => Add(evt.IMEI, evt.ICCID, evt.EncryptionKey, evt.EncryptionSalt));
            Receive<DeviceActor.Events.DeviceDeployed>(evt => Remove(evt.IMEI));
            Receive<DeviceActor.Events.DeviceDeactivated>(evt => Remove(evt.IMEI));

            Receive<SaveCommand>(cmd => Save());
            Receive<PrintState>(cmd => ShowStateOnConsole());
        }
        private void Add(string imei, string iccid, string key, string salt)
        {
            _devices[imei] = new TestDevice { IMEI = imei, ICCID = iccid, Key = key, Salt = salt };
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
                this._devices = JsonConvert.DeserializeObject<Dictionary<string, TestDevice>>(json);
            }
        }
        private void ShowStateOnConsole()
        {
            var s = new StringBuilder();

            s.AppendLine();
            s.AppendLine($"----------------------------------------------------------------");
            s.AppendLine($"----------        Devices Awaiting Deployment         ----------");
            s.AppendLine($"- {"IMEI",15}{"ICCID",15}{"KEY",15}{"SALT",15} -");
            foreach (var d in _devices.Values)
                s.AppendLine($"- {d.IMEI,15}{d.ICCID,15}{d.Key,15}{d.Salt,15} -");
            s.AppendLine($"----------------------------------------------------------------");

            Console.Write(s.ToString());
        }

        void RegisterSubscriptions()
        {
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.DeviceActivated));
            Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.DeviceDeployed));
            //Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.MessageReceived));
            //Context.System.EventStream.Subscribe(Self, typeof(DeviceActor.Events.AlertReceveived));
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

        public class TestDevice
        {
            public string IMEI { get; set; }
            public string ICCID { get; set; }
            public string Key { get; set; }
            public string Salt { get; set; }
        }
    }
}
