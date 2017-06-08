using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UntyptedActorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("demo-server"))
            {
                var service1 = system.ActorOf<DemoActor1>(nameof(DemoActor1));

                Console.ReadKey();
                service1.Tell("This is a message");
                Console.ReadKey();
                service1.Tell(42);
                Console.ReadKey();
                service1.Tell(DateTime.Now);
                Console.ReadKey();
            }
        }
    }
}
