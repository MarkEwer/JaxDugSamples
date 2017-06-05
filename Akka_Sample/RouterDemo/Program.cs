using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterDemo
{
    class Program
    {
        static void Main()
        {
            using (var system = ActorSystem.Create("demo-server"))
            {
                var singleProps = Props.Create<Worker>();
                var single = system.ActorOf(singleProps, "singleWorker");

                var props = Props.Create<Worker>().WithRouter(new RoundRobinPool(15));
                var router = system.ActorOf(props, "multipleWorker");

                SendEchos(single);
                Console.ReadKey();

                SendEchos(router);
                Console.ReadKey();
            }
        }

        static void SendEchos(IActorRef actor)
        {
            foreach(var c in Enum.GetValues(typeof(ConsoleColor)))
            {
                for(int i=0; i<20; i++)
                {
                    var msg = new Echo((ConsoleColor) c, c.ToString() + " Number " + i.ToString());
                    actor.Tell(msg);
                }
            }
        }
    }
}
