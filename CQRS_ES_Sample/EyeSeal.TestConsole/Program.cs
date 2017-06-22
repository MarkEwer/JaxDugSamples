using Akka.Actor;
using Akka.Configuration;
using EyeSeal.Domain.Aggregates;
using EyeSeal.Domain.Messages;
using EyeSeal.Domain.Messages.BreachDetectorSchema;
using EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents;
using EyeSeal.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeSeal.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(GetHocon());

            using (var system = ActorSystem.Create("JaxArchitecture", config))
            {
                var testVm = system.ActorOf<DevicesBeingTested>();
                var activeVm = system.ActorOf<ActiveDevices>();
                IActorRef d1, d2, d3, d4;

                CreateDevices(system, testVm, activeVm, out d1, out d2, out d3, out d4);
                ActivateDevices(testVm, activeVm, d1, d2, d3, d4);
                DeployDevices(testVm, activeVm, d1, d2, d3, d4);
                TrackDeviceMessages(testVm, activeVm, d1, d2, d3, d4);
                RetireDevices(testVm, activeVm, d1, d2, d3, d4);
            }
        }

        private static void CreateDevices(ActorSystem system, IActorRef testVm, IActorRef activeVm, out IActorRef d1, out IActorRef d2, out IActorRef d3, out IActorRef d4)
        {
            Console.WriteLine("*** Creating Device Actors ***");
            d1 = system.ActorOf(Props.Create(typeof(DeviceActor), "Device-1"));
            d2 = system.ActorOf(Props.Create(typeof(DeviceActor), "Device-2"));
            d3 = system.ActorOf(Props.Create(typeof(DeviceActor), "Device-3"));
            d4 = system.ActorOf(Props.Create(typeof(DeviceActor), "Device-4"));
            Console.WriteLine();
            Console.ReadKey();

            testVm.Tell(new PrintState());
            activeVm.Tell(new PrintState());
            Console.ReadKey();
        }
        private static void ActivateDevices(IActorRef testVm, IActorRef activeVm, IActorRef d1, IActorRef d2, IActorRef d3, IActorRef d4)
        {
            Console.WriteLine("*** Activating Devices ***");
            d1.Tell(new DeviceActor.Commands.ActivateDevice(imeis[0], "111111111", "Secret", "Salty!"));
            d2.Tell(new DeviceActor.Commands.ActivateDevice(imeis[1], "222222222", "Secret", "Salty!"));
            d3.Tell(new DeviceActor.Commands.ActivateDevice(imeis[2], "333333333", "Secret", "Salty!"));
            d4.Tell(new DeviceActor.Commands.ActivateDevice(imeis[3], "444444444", "Secret", "Salty!"));
            Console.WriteLine();
            Console.ReadKey();

            testVm.Tell(new PrintState());
            activeVm.Tell(new PrintState());
            Console.ReadKey();
        }
        private static void DeployDevices(IActorRef testVm, IActorRef activeVm, IActorRef d1, IActorRef d2, IActorRef d3, IActorRef d4)
        {
            Console.WriteLine("*** Deploying Devices ***");
            d1.Tell(new DeviceActor.Commands.DeployDevice(DateTime.Now, "Mark", "DiscoverTec", "Bubby's", "Truck", "142", "X", "Nope", "Jax", "Moon", DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(15)));
            d2.Tell(new DeviceActor.Commands.DeployDevice(DateTime.Now, "Charlse", "DiscoverTec", "USPS", "Van", "242", "X", "Nope", "Jax", "Moon", DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(15)));
            d3.Tell(new DeviceActor.Commands.DeployDevice(DateTime.Now, "Robert", "DiscoverTec", "ACME", "Coyote", "342", "X", "Nope", "Jax", "Moon", DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(15)));
            d4.Tell(new DeviceActor.Commands.DeployDevice(DateTime.Now, "Joyti", "Black Night", "Fedex", "Plane", "442", "X", "Nope", "Jax", "Moon", DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(15)));
            Console.WriteLine();
            Console.ReadKey();

            testVm.Tell(new PrintState());
            activeVm.Tell(new PrintState());
            Console.ReadKey();
        }
        private static void TrackDeviceMessages(IActorRef testVm, IActorRef activeVm, IActorRef d1, IActorRef d2, IActorRef d3, IActorRef d4)
        {
            Console.WriteLine("*** Tracking Devices ***");
            //for (int i = 0; i < 10; i++)
            for (int i = 0; i < 100; i++)
            {
                d1.Tell(new DeviceActor.Commands.RecordMessage(imeis[0], GetRandomMessageData().ToJson()));
                d2.Tell(new DeviceActor.Commands.RecordMessage(imeis[1], GetRandomMessageData().ToJson()));
                d3.Tell(new DeviceActor.Commands.RecordMessage(imeis[2], GetRandomMessageData().ToJson()));
                d4.Tell(new DeviceActor.Commands.RecordMessage(imeis[3], GetRandomMessageData().ToJson()));
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadKey();

            testVm.Tell(new PrintState());
            activeVm.Tell(new PrintState());
            activeVm.Tell(new ShowAlerts());
            Console.ReadKey();
        }
        private static void RetireDevices(IActorRef testVm, IActorRef activeVm, IActorRef d1, IActorRef d2, IActorRef d3, IActorRef d4)
        {
            Console.WriteLine("*** Retiring Devices ***");
            d1.Tell(new DeviceActor.Commands.RetireDevice(DateTime.Now.AddDays(72)));
            d2.Tell(new DeviceActor.Commands.RetireDevice(DateTime.Now.AddDays(35)));
            d3.Tell(new DeviceActor.Commands.RetireDevice(DateTime.Now.AddDays(125)));
            d4.Tell(new DeviceActor.Commands.RetireDevice(DateTime.Now.AddDays(15)));
            Console.WriteLine();
            Console.ReadKey();

            testVm.Tell(new PrintState());
            activeVm.Tell(new PrintState());

            Console.ReadKey();
        }



        static string[] imeis = new string[] { "11111142", "22222242", "33333342", "44444442" };
        static int count = 0;

        static BreachDetectorData GetRandomMessageData()
        {
            var bdd = new BreachDetectorData();

            bdd.Id = Guid.NewGuid();
            bdd.Battery = new BatterySchema
            {
                Avg = Faker.NumberFaker.Number(100, 200),
                Cur = Faker.NumberFaker.Number(125, 175),
                Max = Faker.NumberFaker.Number(200, 300),
                Min = Faker.NumberFaker.Number(0, 100)
            };
            bdd.Bds = new BdsSchema
            {
                Lastindex = count++,
                Unitid=Faker.StringFaker.Numeric(11),
                Vers= $"{Faker.NumberFaker.Number(1, 9)}.{Faker.NumberFaker.Number(1, 9)}.{Faker.NumberFaker.Number(1, 9)}.{Faker.NumberFaker.Number(1, 9)}"
            };
            bdd.Doors = new DoorsSchema
            {
                Event=count++,
                Left=new Doorpos
                {
                    Count=Faker.NumberFaker.Number(0,65000),
                    Open=Faker.BooleanFaker.Boolean()
                },
                Right = new Doorpos
                {
                    Count = Faker.NumberFaker.Number(0, 65000),
                    Open = Faker.BooleanFaker.Boolean()
                }
            };
            bdd.Doors.Open = bdd.Doors.Left.Open || bdd.Doors.Right.Open;
            bdd.Humidity = Faker.NumberFaker.Number(20, 100);
            bdd.Lights = new LightsSchema
            {
                Event=Faker.NumberFaker.Number(0,11),
                Frontpass=Faker.BooleanFaker.Boolean(),
                Rearpass=Faker.BooleanFaker.Boolean(),
                Left=new Lightpos
                {
                    Count= Faker.NumberFaker.Number(0, 65000),
                    Lit = Faker.BooleanFaker.Boolean()
                },
                Right= new Lightpos
                {
                    Count = Faker.NumberFaker.Number(0, 65000),
                    Lit = Faker.BooleanFaker.Boolean()
                },
                Rear= new Lightpos
                {
                    Count = Faker.NumberFaker.Number(0, 65000),
                    Lit = Faker.BooleanFaker.Boolean()
                }
            };
            bdd.Lights.Lit = bdd.Lights.Left.Lit || bdd.Lights.Right.Lit || bdd.Lights.Rear.Lit;
            bdd.Location = GetFakeLocation();
            bdd.Lognum = count++;
            bdd.Radio = new RadioSchema
            {
                Bert = Faker.NumberFaker.Number(0, 65000),
                Rssi = Faker.NumberFaker.Number(0, 65000),
                Imei=imeis[Faker.NumberFaker.Number(0,3)],
                Vers=bdd.Bds.Vers
            };
            bdd.Seal = new SealSchema
            {
                Closed = new Sealstate
                {
                    Location= GetFakeLocation(),
                    Time=Faker.NumberFaker.Number(210000, 250000)
                },
                Confirmed = new Sealstate
                {
                    Location= GetFakeLocation(),
                    Time=Faker.NumberFaker.Number(210000, 250000)
                },
                Sealed = Faker.BooleanFaker.Boolean()
            };
            bdd.Tdata = new TdataSchema
            {
                Conn = Faker.NumberFaker.Number(0, 65000),
                Gprsr = Faker.NumberFaker.Number(0, 65000),
                Gsmr = Faker.NumberFaker.Number(0, 65000),
                Recovery = Faker.NumberFaker.Number(0, 65000)
            };
            bdd.Tempf = Faker.NumberFaker.Number(0, 100);
            bdd.Timeval = Faker.NumberFaker.Number(0, 65000);
            bdd.Log = new List<LogSchema>();
            for (int i = 0; i < Faker.NumberFaker.Number(1, 10); i++)
                bdd.Log.Add(GetFakeLogEntry());
            return bdd;
        }
        static LocSchema GetFakeLocation()
        {
            return new LocSchema
            {
                Cid = Faker.NumberFaker.Number(0, 65000),
                Lac = Faker.NumberFaker.Number(0, 65000),
                Mcc = Faker.NumberFaker.Number(0, 65000),
                Mnc = Faker.NumberFaker.Number(0, 65000)
            };
        }
        static LogSchema GetFakeLogEntry()
        {
            var log = new LogSchema
            {
                Errnum = Faker.NumberFaker.Number(0, 10),
                Index = count++,
                Timeval = Faker.NumberFaker.Number(0, 65000),
                Supdat = GetFakeEvent()
            };
            log.Errdesc = log.Supdat.GetType().Name;

            return log;
        }
        static EventBase GetFakeEvent()
        {
            var selector = Faker.NumberFaker.Number(0, 5);
            switch (selector)
            {
                case 0:
                    var e = new DoorEvent
                    {
                        Eventno = count++,
                        EventType = DoorEventEventType.DoorEvent,
                        Doors = new DoorsSchema
                        {
                            Event = count++,
                            Left = new Doorpos
                            {
                                Count = Faker.NumberFaker.Number(0, 65000),
                                Open = Faker.BooleanFaker.Boolean()
                            },
                            Right = new Doorpos
                            {
                                Count = Faker.NumberFaker.Number(0, 65000),
                                Open = Faker.BooleanFaker.Boolean()
                            }
                        }
                    };
                    return e;
                case 1:
                    var le = new LightEvent
                    {
                        Eventno = count++,
                        EventType = LightEventEventType.LightEvent,
                        Lights = new LightsSchema
                        {
                            Event = Faker.NumberFaker.Number(0, 11),
                            Frontpass = Faker.BooleanFaker.Boolean(),
                            Rearpass = Faker.BooleanFaker.Boolean(),
                            Left = new Lightpos
                            {
                                Count = Faker.NumberFaker.Number(0, 65000),
                                Lit = Faker.BooleanFaker.Boolean()
                            },
                            Right = new Lightpos
                            {
                                Count = Faker.NumberFaker.Number(0, 65000),
                                Lit = Faker.BooleanFaker.Boolean()
                            },
                            Rear = new Lightpos
                            {
                                Count = Faker.NumberFaker.Number(0, 65000),
                                Lit = Faker.BooleanFaker.Boolean()
                            }
                        }
                    };
                    return le;
                case 2:
                    var re = new RelocatedEvent
                    {
                        EventType = RelocatedEventEventType.RelocatedEvent,
                        Location = GetFakeLocation()
                    };
                    return re;
                case 3:
                    var pe = new PowerEvent
                    {
                        EventType = PowerEventEventType.PowerEvent,
                        Msg = Faker.TextFaker.Sentence()
                    };
                    return pe;
                case 4:
                    var ce = new CommEvent
                    {
                        EventType = CommEventEventType.CommEventSuccess,
                        Conncount = count++,
                        Humidity = Faker.NumberFaker.Number(0, 100),
                        Tempf = Faker.NumberFaker.Number(0, 100),
                        Msgnum = count,
                        Msg = Faker.TextFaker.Sentence()
                    };
                    return ce;
                case 5:
                    var cee = new CommEvent
                    {
                        EventType = CommEventEventType.CommEventError,
                        Conncount = count++,
                        Humidity = Faker.NumberFaker.Number(0, 100),
                        Tempf = Faker.NumberFaker.Number(0, 100),
                        Msgnum = count,
                        Msg = Faker.TextFaker.Sentence()
                    };
                    return cee;
                default:
                    return null;
            }
        }

        static string GetHocon()
        {
            var hocon = @"akka {
                            suppress-json-serializer-warning = on
                            persistence {
                                publish-plugin-commands = on
                                journal {
                                    plugin = ""akka.persistence.journal.sql-server""
                                    sql-server {
                                        class = ""Akka.Persistence.SqlServer.Journal.SqlServerJournal, Akka.Persistence.SqlServer""
                                        plugin-dispatcher = ""akka.actor.default-dispatcher""
                                        table-name = EventJournal
                                        schema-name = dbo
                                        auto-initialize = on
                                        connection-string-name = ""EyeSeal_CQRS""
                                    }
                                }
                            
                                snapshot-store {
                                    plugin = ""akka.persistence.snapshot-store.sql-server""
                                    sql-server {
                                        class = ""Akka.Persistence.SqlServer.Snapshot.SqlServerSnapshotStore, Akka.Persistence.SqlServer""
                                        plugin-dispatcher = ""akka.actor.default-dispatcher""
                                        table-name = SnapshotStore
                                        schema-name = dbo
                                        auto-initialize = on
                                        connection-string-name = ""EyeSeal_CQRS""
                                    }
                                }
                            }    
                          }";
            return hocon;
        }
    }
}
