using Akka.Actor;
using System;

namespace RouterDemo
{
    public class Worker:ReceiveActor
    {
        public Worker()
        {
            Receive<Echo>(x => EchoMessage(x));
            //Receive<Echo>(x => EchoMessageWithThreadingError(x));
        }
        private void EchoMessage(Echo x)
        {
            Console.ForegroundColor = x.Color;
            Console.WriteLine($"ACTOR: {Self.Path.Name} - MESSAGE: {x.Message}");
            Console.ResetColor();
        }
        private void EchoMessageWithThreadingError(Echo x)
        {
            foreach(char c in $"ACTOR: {Self.Path.Name} - MESSAGE: {x.Message}".ToCharArray())
            {
                Console.ForegroundColor = x.Color;
                Console.Write(c);
                Console.ResetColor();
            }
            Console.Write('\n');
        }
    }
}




