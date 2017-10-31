# JaxDug Samples
In this repository I store the sample code and presentation slides for the talks I have given at 
[JaxDug](https://www.meetup.com/Jaxdug/).

## Transitioning Legacy Procedural into Object Oriented Code 
In this presentation, Mark will walk you through how to convert the kinds of procedural code typically found in a legacy application into modern, object-oriented structures.  Along the way, we'll point out some of the new features of C# 6 and highlight testability, maintainability, and make your code easier to modify in the future.

This presentation was given at the JaxDug meetup on November 9th, 2017.

 1. The difference between procedural code and object-oriented code
 1. Converting boolean conditionals into object branching 
 1. Converting procedural iterators into object selectors 
 1. Converting procedural algorithms into object strategies

Source code for this is in the [Refactoring Procedural Code](https://github.com/MarkEwer/JaxDugSamples/tree/master/Refactoring_Procedural_Code) folder.

## CQRS and DDD
This presentation shows how to implement a CQRS system and a couple of frameworks you can use on the .Net platform.  This is the presentation I gave at [Code Impact 2017](http://www.codeimpact.org).  This folder contains the sample code an the slides from my presentation but you can find some additional information and a good description of the sample code's architecture on [My Web Site](http://www.markewer.com/2016/11/14/cqrs-system-design/).

## Evolution of CQRS+ES
This presentation attempted to show how the CQRS and ES design patterns are natural evolutionary steps to 
when you are growing an application.  Separating the commands from the queries in a system is a way to really
enhance the overall system performance while Event Sourcing is a response to the complexity of cache item
invalidation.

You can find the sample code and presentation in the 
[CQRS ES Sample folder](https://github.com/MarkEwer/JaxDugSamples/tree/master/CQRS_ES_Sample).

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

