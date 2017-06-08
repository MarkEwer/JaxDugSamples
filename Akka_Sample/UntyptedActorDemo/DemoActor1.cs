using Akka.Actor;
using System;

namespace UntyptedActorDemo
{




    public class DemoActor1 : UntypedActor
    {
        private int _counter = 0;
        protected override void OnReceive(object message)
        {
            if (message is string) Console.WriteLine(message);
            else if (message is int)
            {
                _counter += (int)message;
                Console.WriteLine($"Actor has accumulated {_counter} points.");
            }
            else { Console.WriteLine("What?"); }
        }
    }




}
