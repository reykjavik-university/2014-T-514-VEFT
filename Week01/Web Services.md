# Chapter 1

## Historical background

Traditional web applications (ASP, PHP, ASP.NET WebForms/MVC, Ruby on Rails etc.) are usually applications where everything is rendered server-side, and the client (i.e. the web browser) receives an HTML document. This has architecture has a number of pros:

* the client can be "lightweight", i.e. it only has to render the HTML
* if the application has to be modified, it can be done in a single place, i.e. on the server

However, there are also cons:

* this may result in a lot of overhead being passed between client and server, i.e. a small change in a page may result in a full page reload
* if the client isn't a web browser, it will have to parse the result and extract exactly the data it needs to display, this is fragile and error-prone.

In recent years, a different architecture has become increasingly popular: the server doesn't render a full HTML page, but only delivers the data. The client must then take care of render the data, usually as HTML, but other clients (i.e. iOS/Android apps) can use different rendering methods.

## Types of web services

There are a number of different types of web services which have been developed over the course of years:

* SOAP
* REST
* WCF

Recently, REST services are becoming more and more popular. We will spend most time looking at how to implement a RESTful web service using ASP.NET Web API.
