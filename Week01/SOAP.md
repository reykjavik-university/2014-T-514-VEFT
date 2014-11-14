# SOAP

## What.

SOAP originally stood for Simple Object Access Protocol but that acronym was later drooped, this protocol is used to exchange structured information in web services. 

One of SOAP characteristics is its heavy use of the Xml information set. Other characteristics are extensibility, neutrality and independence.

SOAP was initialy designed in 1998 by Dave Winer, Don Box, Bob Atkinson, and Mohsen Al-Ghosein for Microsoft.

## Why.

SOAP was Created to communicate between applications over HTTP. This is a very smart thing to do since HTTP is supported by all Internet browsers and servers. 

With this form of communication the operating system your application is running on or the programing language it is written in is no longer apart of the equation. That is, no longer a constraint on your system design. 

## Why not.

There is this thing called REST.

## REST vs SOAP

It is becoming quite apparent that the RESTful Json APIs are becoming more popular than the SOAP ones. One of the biggest advantages REST has over SOAP is its usage of standard http. 

REST uses many different data formats while SOAP only uses the boring old Xml. Rest also has better performance and scalability. 

At this rate it sounds a little one-sided argument but SOAP has a few advanteges over REST like WS-Security(identity through intermediaries), WS-AtomicTransaction (ACID compliant transactions) and WS-ReliableMessaging(successful/retry logic).

But in the end it is quite apparent that REST is the future and SOAP is slowely becoming history.

## Helpful resources

SOAP example Request and Response:
http://www.w3schools.com/webservices/ws_soap_example.asp

SOAP tutorial:
http://www.tutorialspoint.com/soap/

REST vs SOAP:
http://spf13.com/post/soap-vs-rest
