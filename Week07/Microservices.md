# Microservices

The term Microservices has been described by Martin Fowler: 
[http://martinfowler.com/articles/microservices.html](http://martinfowler.com/articles/microservices.html).
In this architecture, the application is built upon many small services, where each service is a single
deployable unit, with its own database. Each service has its own (REST) API, and if service A requires data
from service B, it will only use the public API to access that data. 

This architecture has a number of advantages:

* Smaller services are easier to manage
* They can be scaled up, i.e. if a given service is used more than others, it can be deployed on more machines.
  This could result in some microservices being deployed on a single machine, while other would be deployed
  on a number of machines.
  
There are of course downsides as well, some of which are outlined in this article:
[http://www.stackbuilders.com/news/the-hidden-costs-of-microservices](http://www.stackbuilders.com/news/the-hidden-costs-of-microservices)

* If some operations need to be atomic, but cross more than one microservice, we can no longer use traditional
  transaction support provided by databases (since the operations span multiple individual databases).
* All microservice require a certain amount of "boilerplate" code to support various common tasks such as
  documentation, error handling, logging and authentication, and this code may have to be duplicated.
* When a microservice returns a list of some items, and each item requires access to data defined in another
  microservice, then the client may end up doing (N+1) requests, i.e. 1 to fetch the list of items, and N
  requests for the additional data.

There are ways around these problems, but no solutions are trivial.
