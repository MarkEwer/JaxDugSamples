using Akka.Actor;
using Akka.TestKit.NUnit3;
using NUnit.Framework;
using Server.Actors;
using Server.Commands;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using Server.Commands.Franchise;

namespace Server_Tests
{
    [TestFixture]
    public class TestFranchise : TestKit
    {
        [TearDown]
        public void Cleanup()
        {
            this.Shutdown();
        }

        // This task has an AWAITED method call, so it must be made into an
        // ASYNC method.
        [Test]
        public async Task TestFranchiseCreation()
        {
            // TestKit provides a built-in actor system that it can manage
            // for you to ensure proper startup/shutdown and test isolation
            var franchise = Sys.ActorOf(FranchiseActor.Props("UnitTest001"));
            franchise.Tell(new RegisterInterestedFranchisee("Unit Test", "Unit", "Test",
                new Server.ValueObjects.StreetAddress("873 Branscomb Rd", "Green Cove Springs", "FL", "32043", "USA")));

            // This tests communicates with the Actor using the ASK() pattern
            // This only works if you exepect a single message reply and you
            // don't care about causing a blocking test if the actor fails to
            // send a reply.
            var entity = await franchise.Ask<Franchise>(new ShareState());
            entity.ShouldNotBeNull();
        }

        [Test]
        public void TestFranchiseRegistration()
        {
            var franchise = Sys.ActorOf(FranchiseActor.Props("UnitTest002"));
            
            // This test uses the TESTACTOR built into the testing framework to
            // send a message to the FranchiseActor and wait for an expected
            // response message.  This is the preferred setup because we can 
            // add a timeout to prevent 'Hanging' test cases.
            franchise.Tell(new ShareState(), this.TestActor);

            // Now we can use the built in methods for ExpectMsg() and ExpectNoMsg()
            // to validate the results are as expected.  In this case, the Actor has
            // not been given any data about the Franchise it is managing, so it will
            // not respond to the request to share its internal state.
            ExpectNoMsg();

            // If we initialize the Franchise, then we should change state from
            // INITIALSTATE to the INACTIVE state.  In the INACTIVE state, the
            // actor should respond to the request to share its internal state.
            franchise.Tell(new RegisterInterestedFranchisee("Unit Test", "Unit", "Test", 
                new Server.ValueObjects.StreetAddress("873 Branscomb Rd", "Green Cove Springs", "FL", "32043", "USA")));
            franchise.Tell(new ShareState(), this.TestActor);

            //Wait up to 5 seconds for the response.
            var entity = ExpectMsg<Franchise>(new TimeSpan(0,0,5));

            entity.ID.ShouldBeSameAs("UnitTest002");
            entity.MailingAddress.State.ShouldBeSameAs("FL");
        }

        [Test]
        public void TestOpening()
        {
            var franchise = Sys.ActorOf(FranchiseActor.Props("UnitTest003"));
            franchise.Tell(new RegisterInterestedFranchisee("Unit Test", "Unit", "Test",
                new Server.ValueObjects.StreetAddress("879 Branscomb Rd", "Green Cove Springs", "FL", "32043", "USA")),
                this.TestActor);

            franchise.Tell(new ShareState(), this.TestActor);
            var entity = ExpectMsg<Franchise>();
            entity.ID.ShouldBeSameAs("UnitTest003");
            entity.Name.ShouldBeSameAs("Unit Test");

            franchise.Tell(new RegisterFranchiseTaxId("UnitTest003", "TaxId42"), 
                this.TestActor);

            franchise.Tell(new ShareState(), this.TestActor);
            entity = ExpectMsg<Franchise>();
            entity.TaxId.ShouldBeSameAs("TaxId42");

            franchise.Tell(new SignFranchiseAgreement("UnitTest003", new DateTime(2017, 4, 1)), 
                this.TestActor);

            franchise.Tell(new ShareState(), this.TestActor);
            entity = ExpectMsg<Franchise>();
            entity.AgreementSigned.ShouldBeTrue();
            entity.DateOfFormation.Year.ShouldEqual(2017);
            entity.DateOfFormation.Month.ShouldEqual(4);
            entity.DateOfFormation.Day.ShouldEqual(1);
        }
    }
}
