# SOAP

## What.

SOAP originally stood for Simple Object Access Protocol but that acronym was later dropped, this protocol is used to exchange structured information in web services. 
One of SOAP characteristics is its heavy use of the Xml information set. Other characteristics are extensibility, neutrality and independence. 

SOAP was initially designed in 1998 by Dave Winer, Don Box, Bob Atkinson, and Mohsen Al-Ghosein for Microsoft.

## Why.

SOAP was Created to communicate between applications over HTTP. This is a very smart thing to do since HTTP is supported by all Internet browsers and servers.  With this form of communication the operating system your application is running on or the programing language it is written in is no longer apart of the equation. That is, no longer a constraint on your system design. 

Most SOAP service URI's expose all functions you can use, so it is easy to lookup available actions. SOAP also offers a few things that REST does not, namely:
####WS-Security
Adds enterprise security features, such as domain authentication amongst other things.

####WS-AtomicTransactions
SOAP supports ACID transactions which is sometimes useful and used in enterprise applications, the everyday web developer will however never need this.

####WS-ReliableMessaging
SOAP has success/retry logic built in and provides end-to-end reliability while REST relies on clients to take care of retrying.



As you can see SOAP has some capabilities REST does not have, however they are mostly benificial in enterprise applications, where a more complex version of the SOAP protocol is used, Web Services Description Language or WSDL. Almost all banking and financial web services are WSDL services, which are based on SOAP.

## Why not.

SOAP services only return Xml data which has more overhead than Json, so data transfers can be significantly slower. SOAP is also alot more verbose than REST, since all actions are bound to a single function call.

## REST vs SOAP


It is becoming quite apparent that the RESTful Json APIs are becoming more popular than the SOAP ones. One of the biggest advantages REST has over SOAP is its usage of standard http. 

REST services can return data in various formats, both Xml and Json, depending on what the client asks for, while SOAP only returns Xml. As stated before, Xml has alot more overhead than Xml so REST services perform faster and scale better.

REST web services are slowly taking over the world, they are easier to develope, are less verbose and perform better. However there is still a place for SOAP services, as previously pointed out, they are used widely in .NET enterprise systems. Almost all Icelandic banks and financial companies use WSDL(SOAP) services.

## Helpful resources

SOAP example Request and Response:
http://www.w3schools.com/webservices/ws_soap_example.asp

SOAP tutorial:
http://www.tutorialspoint.com/soap/

REST vs SOAP:
http://spf13.com/post/soap-vs-rest
