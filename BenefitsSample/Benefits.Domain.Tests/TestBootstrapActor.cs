using System;
using Akka.Actor;

namespace Benefits.Domain.Tests
{
    public class TestBootstrapActor:ReceiveActor
    {
        public TestBootstrapActor()
        {
            Receive<string>(x =>
            {
                System.Diagnostics.Debug.WriteLine(x);
                Sender.Tell("Message Received", Self);
            });
        }
    }

    public class TestUntyptedActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            System.Diagnostics.Debug.WriteLine($"Received Message: {message.ToString()}");
            Sender.Tell(OperationResult.Success(nameof(TestUntyptedActor)));
        }
    }
}
