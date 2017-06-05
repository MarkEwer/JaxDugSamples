using Akka.Actor;
using Akka.Configuration;
using StateMachineShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineClient
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
                		    port = 0
                		    hostname = localhost
                        }
                    }
                }
                ");

            using (var system = ActorSystem.Create("lamp-client", config))
            {
                var lamp = system.ActorSelection("akka.tcp://lamp-server@localhost:8081/user/LampActor");

                TellMessagesToRemoteActor(lamp);
                SendMessagesWithScheduler(system, lamp);

                Console.ReadKey();

            }
        }

        private static void SendMessagesWithScheduler(ActorSystem system, ActorSelection lamp)
        {
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(2), lamp, new LampActor.UnPlug(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(3), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(4), lamp, new LampActor.PressPowerButton(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(5), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(6), lamp, new LampActor.PlugIn(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(7), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(8), lamp, new LampActor.PlugIn(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(9), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(10), lamp, new LampActor.PressPowerButton(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(11), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(12), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(13), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(14), lamp, new LampActor.PlugIn(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(15), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(16), lamp, new LampActor.PressPowerButton(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(17), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(18), lamp, new LampActor.PressPowerButton(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(19), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(20), lamp, new LampActor.PressPowerButton(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(21), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(22), lamp, new LampActor.PressPowerButton(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(23), lamp, new LampActor.QueryState(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(24), lamp, new LampActor.UnPlug(), Nobody.Instance);
            system.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(25), lamp, new LampActor.QueryState(), Nobody.Instance);
        }

        private static void TellMessagesToRemoteActor(ActorSelection lamp)
        {
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
        }
    }
}
