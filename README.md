# JaxDug Samples
In this repository I store the sample code and presentation slides for the talks I have given at 
[JaxDug](https://www.meetup.com/Jaxdug/).

## WebAssembly with OOUI and Blazor (May 10th, 2018)
In this presentation I show an overview of Web Assembly with demos of OOUI and Blazor. The presentation will cover how to use the Mono compiler toolchain to build, deploy, and run .Net assemblies in the web browser. The focus is on the bleeding-edge work being done by several open source projects and will, hopefully, show everyone how and why you may want to invest some time learning about this exciting new technology stack addition.

 - [Sample Code](https://github.com/MarkEwer/JaxDugSamples/tree/master/WebAssembly)
 - [Slides](https://github.com/MarkEwer/JaxDugSamples/blob/master/WebAssembly/JaxDug_WebAssembly_20180510.pptx)
 - [Meetup](https://www.meetup.com/jaxdug/events/gsctdpyxhbnb/)

## Transitioning Legacy Procedural into Object Oriented Code (Nov 9th, 2017)
In this presentation, Mark will walk you through how to convert the kinds of procedural code typically found in a legacy application into modern, object-oriented structures.  Along the way, we'll point out some of the new features of C# 6 and highlight testability, maintainability, and make your code easier to modify in the future.

This presentation was given at the JaxDug meetup on November 9th, 2017.

 1. The difference between procedural code and object-oriented code
 1. Converting boolean conditionals into object branching 
 1. Converting procedural iterators into object selectors 
 1. Converting procedural algorithms into object strategies
 1. Converting procedures to simple task execution pipelines

 - [Sample Code](https://github.com/MarkEwer/JaxDugSamples/tree/master/Refactoring_Procedural_Code)
 - [Slides](https://github.com/MarkEwer/JaxDugSamples/raw/master/Refactoring_Procedural_Code/Refactoring_Procedural_Code.pptx)
 - [Meetup](https://www.meetup.com/jaxdug/events/244688610/)

## CQRS and DDD (Aug 26th, 2017)
This presentation shows how to implement a CQRS system and a couple of frameworks you can use on the .Net platform.  This is the presentation I gave at [Code Impact 2017](http://www.codeimpact.org).  This folder contains the sample code an the slides from my presentation but you can find some additional information and a good description of the sample code's architecture on [My Web Site](http://www.markewer.com/2016/11/14/cqrs-system-design/).

 - [Sample Code](https://github.com/MarkEwer/JaxDugSamples/tree/master/BenefitsEstimation)
 - [Slides](https://github.com/MarkEwer/JaxDugSamples/raw/master/BenefitsEstimation/CodeImpact_Presentation.pptx)
 - [Code Impact](http://www.codeimpact.org)

## Evolution of CQRS+ES (June 22nd, 2017)
This presentation attempted to show how the CQRS and ES design patterns are natural evolutionary steps to 
when you are growing an application.  Separating the commands from the queries in a system is a way to really
enhance the overall system performance while Event Sourcing is a response to the complexity of cache item
invalidation.

 - [Sample Code](https://github.com/MarkEwer/JaxDugSamples/tree/master/CQRS_ES_Sample)
 - [Slides](https://github.com/MarkEwer/JaxDugSamples/raw/master/CQRS_ES_Sample/JaxSig_Evolving_to_CQRS_And_ES.pptx)
 - [Meetup](https://www.meetup.com/JaxArcSIG/events/238015957/)

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

 - [Sample Code](https://github.com/MarkEwer/JaxDugSamples/tree/master/Akka_Sample)
 - [Slides](https://github.com/MarkEwer/JaxDugSamples/raw/master/Akka_Sample/JaxDug_Presentation_Akka_Overview.pptx)
 - [Meetup](https://www.meetup.com/jaxdug/events/240110328/)

