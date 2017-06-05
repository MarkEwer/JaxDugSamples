using Akka.Actor;
using System;

namespace RouterDemo
{
    public class Worker:ReceiveActor
    {
        public Worker()
        {
            Receive<Echo>(x => EchoMessage(x));
        }
        private void EchoMessage(Echo x)
        {
            Console.ForegroundColor = x.Color;
            Console.WriteLine($"ACTOR: {Self.Path.Name} - MESSAGE: {x.Message}");
            Console.ResetColor();
        }
    }
}




            //foreach(char c in $"ACTOR: {Self.Path.Name} - MESSAGE: {x.Message}".ToCharArray())
            //{
            //    Console.Write(c);
            //}
            //Console.Write('\n');
