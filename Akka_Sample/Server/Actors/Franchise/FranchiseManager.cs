using Akka.Actor;
using Server.Commands;
using Server.Commands.Franchise;
using Server.Exceptions.Franchise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Actors.Franchise
{
    public class FranchiseManager:ReceiveActor
    {
        public FranchiseManager()
        {
            Receive<CreateFranchise>(x => AddFranchiseActor(x));
            Receive<GetFranchise>(x => GetChildFranchiseActorRef(x));
            Receive<FindFranchise>(x => SearchChildren(x));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy( //or AllForOneStrategy
            maxNrOfRetries: 10,
            withinTimeRange: TimeSpan.FromSeconds(30),
            decider: Decider.From(x =>
            {
                // if the error is because the franchise is not active yet, 
                // then we can just resume.
                if (x is InactiveFranchiseException) return Directive.Resume;

                // if it is an unknown francise id, then we have a message
                // that has been routed to the wrong actor, so the error
                // is really in the caller.
                else if (x is UnknownFranchiseException) return Directive.Resume;

                // For other problems, we should reboot the actor.  This will
                // re-load it from persistance (if this demo had persistance enabled)
                else return Directive.Restart;
            }));
        }

        private void AddFranchiseActor(CreateFranchise child)
        {
            var newFranchise = Context.ActorOf<FranchiseActor>(child.Id.ToString("N"));
            Sender.Tell(newFranchise);
            Console.WriteLine($"Created new FranchiseActor called {newFranchise.Path.Name}");
        }
        private void GetChildFranchiseActorRef(GetFranchise franchise)
        {
            var found = Context.GetChildren()
                               .FirstOrDefault(x => x.Path.Name.Contains(franchise.Id.ToString("N")));
            if (found != null)
            {
                Sender.Tell(found);
                Console.WriteLine($"Found FranchiseActor called {found.Path.Name}");
            }
            else
            {
                Sender.Tell(Nobody.Instance);
                Console.WriteLine($"Could not find FranchiseActor called {franchise.Id.ToString("N")}");
            }
        }
        private void SearchChildren(FindFranchise search)
        {
            var found = Context.GetChildren()
                               .Where(x => x.Path.Name.Contains(search.SearchTerm));
            if (found != null)
            {
                Sender.Tell(found);
                Console.WriteLine($"Found {found.Count()} machting franchises");
            }
            else
            {
                Sender.Tell(Nobody.Instance);
                Console.WriteLine($"Could not find any FranchiseActors to match {search.SearchTerm}");
            }
        }
    }
}
