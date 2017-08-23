using Akka.Actor;
using Akka.TestKit.NUnit3;
using NUnit.Framework;

namespace Benefits.Domain.Tests
{
    [TestFixture]
    public class BootstrapTest : TestKit
    {
        [Test]
        public void ValidateSystemBootstrap()
        {
            var bootstrap = Sys.ActorOf(Props.Create<TestBootstrapActor>());
            bootstrap.Tell("Test");
            var response = ExpectMsg<string>();
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void ValidateUntyptedActor()
        {
            var bootstrap = Sys.ActorOf(Props.Create<TestUntyptedActor>());
            bootstrap.Tell("Test");
            var response = ExpectMsg<Domain.OperationResult>();
            Assert.IsNotNull(response);
        }
    }
}
