using Akka.Actor;
using Akka.Configuration;
using StateMachineShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
                akka {  
                    actor {
                        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                    }
                    remote {
                        dot-netty.tcp {
                            port = 8081
                            hostname = 0.0.0.0
                            public-hostname = localhost
                        }
                    }
                }
                ");
            
            using (var system = ActorSystem.Create("lamp-server", config))
            {
                var lamp = system.ActorOf<LampActor>(nameof(LampActor));

                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.UnPlug());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PressPowerButton());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PlugIn());

                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PlugIn());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PressPowerButton());

                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PlugIn());

                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PressPowerButton());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PressPowerButton());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PressPowerButton());
                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.PressPowerButton());

                lamp.Tell(new LampActor.QueryState());
                lamp.Tell(new LampActor.UnPlug());
                lamp.Tell(new LampActor.QueryState());

                Console.ReadKey();
            }
        }
    }
}
