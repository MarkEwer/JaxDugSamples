# JaxDug Samples
In this repository I store the sample code and presentation slides for the talks I have given at 
[JaxDug](https://www.meetup.com/Jaxdug/).

## Overview and Demo of Akka.Net (June 8th, 2017)

The first presentation is on the [Akka.Net framework](http://getakka.net/).  Akka.net is an implementation of 
the Actor Model system inspired by the Java/Scala framework called Akka.  Akka.net gives you a way to build 
highly concurrent, distributed, fault-tolerant, and event-driven applications on the .Net platform.  Mark 
will demonstrate how to create an Actor and start the Actor System.  He will show you how to implement 
fault-tolerance with an Actor supervision hierarchy.  The demonstration will also include an example of 
configuring a finite state machine implementation for an Actor with the actor’s internal behavior changing 
depending on the state.  Lastly, he will discuss setting up location transparency for actors to enable 
distributed processing.  If time permits, he will touch on how Akka.net, and actor systems in general, lend 
themselves to a CQRS and Event Sourcing as a method of data persistence.

You can find the sample code and presentation slides for this in the 
[Akka_Sample folder](https://github.com/MarkEwer/JaxDugSamples/tree/master/Akka_Sample).