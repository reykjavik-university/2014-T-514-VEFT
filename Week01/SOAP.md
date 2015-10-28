# SOAP

## Background

SOAP originally stood for Simple Object Access Protocol but that acronym was later dropped, this protocol is used to exchange structured information in web services. 

SOAP was initialy designed in 1998 by Dave Winer, Don Box, Bob Atkinson, and Mohsen Al-Ghosein at Microsoft as an object-access protocol.

One of SOAP characteristics is its heavy use of the XML information set. Other characteristics are extensibility, neutrality and independence. Amidst that, it is stateless


### Pros

* Language neutrality. SOAP is not dependant on a specific language for development.
* As a result of its language neutrality, a service following the SOAP principle can be platform independant. 
* Human readability. SOAP messages are in very simple XML format.
* Scalability. SOAP, like services following other principles such as Rest, uses HTTP for its communication. And so, can be scaled in the same manner.

### Cons

* SOAP services come with a larger overhead than say Rest, and as a result, tend to run slower. In the event that SOAP's de-facto content type of XML is less preferrable to the communicating client than say, Json, it needs to be parsed by the client.
* WSDL Dependence. SOAP relis on WSDL (Web Services Description Language) for descriptions of its available resources, and does not have any standardized mechanism for dynamic discovery of its available services.

## REST vs SOAP

###### Content Type
Whereas Rest is inherently flexible when it comes to returning differing dataformats, such as Json, or XML, the SOAP design is constricted to XML. Depending on desired result, this may yield better performance due to less overhead.

###### Security and Data Integrity
At this rate it sounds a little one-sided argument but SOAP has a few advanteges over REST like WS-Security(identity through intermediaries), WS-AtomicTransaction (ACID compliant transactions) and WS-ReliableMessaging(successful/retry logic).

In the event ACID transactions are needed over a service, SOAP tends to be the preferred pick. While Restful supports these transactions, they aren't ACID compliant, nor as comprehensive. REST is limited by the HTTP communication form itself which can’t provide two-phase commits across distributed transactional resources. SOAP can however. Though generally, such transactions over the wire find little logical place in most applications, such as ones applying REST principles, it is often a desired mechanic in Enterprise applications.

Rest doesn’t have a standard messaging system and expects clients to deal with communication failures by retrying. SOAP has successful/retry logic built into it which allows for end-to-end reliability even through SOAP intermediaries.

#### Bottomline
While SOAP's design approach may be slightly more dated than the one applied in REST services, they both come with logical pros and cos, which are more attuned to certain conditions and requirements. And so, arguments can be made for the usage of both depending on the nature of the application.


## Sources 

What is SOAP?
http://careerride.com/SOAP-What-is-SOAP.aspx

SOAP example Request and Response:
http://www.w3schools.com/webservices/ws_soap_example.asp

SOAP tutorial:
http://www.tutorialspoint.com/soap/

REST vs SOAP:
http://spf13.com/post/soap-vs-rest


