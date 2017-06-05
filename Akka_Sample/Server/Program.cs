using Akka.Actor;
using Akka.Remote;
using Server.Actors.Franchise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var system = ActorSystem.Create("franchise-management-server"))
            {
                var service1 = system.ActorOf<FranchiseManager>(nameof(FranchiseManager));
                Console.ReadKey();
            }
        }
    }
}
