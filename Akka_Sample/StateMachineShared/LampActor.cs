using Akka.Actor;
using System;

namespace StateMachineShared
{
    public class LampActor : ReceiveActor
    {
        string _state;

        public LampActor()
        {
            this.Become(Unpowered);
        }

        #region States
        private void BaseActions()
        {
            Receive<QueryState>(x => Console.WriteLine(this._state));
            ReceiveAny(x => Log($"The Command {x.GetType().Name} is not valid in the {_state} State."));
        }
        private void Unpowered()
        {
            _state = nameof(Unpowered);
            Receive<PlugIn>(x => this.Become(Powered));
            BaseActions();
        }
        private void Powered()
        {
            _state = nameof(Powered);
            Receive<UnPlug>(x => this.Become(Unpowered));
            Receive<PressPowerButton>(x => this.Become(LightOn));
            BaseActions();
        }
        private void LightOn()
        {
            _state = nameof(LightOn);
            Receive<UnPlug>(x => this.Become(Unpowered));
            Receive<PressPowerButton>(x => this.Become(Powered));
            BaseActions();
        }
        #endregion States

        #region Actions
        private void Log(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        #endregion Actions

        #region Messages
        public class PlugIn { }
        public class UnPlug { }
        public class PressPowerButton { }
        public class QueryState { }
        #endregion Messages
    }
}
