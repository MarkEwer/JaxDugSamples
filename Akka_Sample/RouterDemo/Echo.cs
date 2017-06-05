using System;

namespace RouterDemo
{
    public class Echo
    {
        public string Message { get; private set; }
        public ConsoleColor Color { get; private set; }
        public Echo(ConsoleColor color, string message)
        {
            this.Message = message;
            this.Color = color;
        }
    }
}
