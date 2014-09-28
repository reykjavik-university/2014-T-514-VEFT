# OData

OData or Open Data Protocol is a protocol build on HTTP which enables clients to access data in a uniform maner using URLs. As the name implies, the OData services exposes the data to clients, that is, it enables client to CRUD data models through http messages.

The OData protocol supports data model descriptions and manipulation of the data. As description by the protocol specs, This is achieved through the following facilities: 

* Metadata:      The description of the data model
* Data:          The data sets
* Querying:      This enables for filtering and other transformations on the data
* Editing:       CRUD the data
* Operations:    The represents some custom logic or functions on the data
* Vocabularies:  Attaching custom semantics

The full documentation for the OData protocol can be found [here](http://docs.oasis-open.org/odata/odata/v4.0/odata-v4.0-part1-protocol.html)


The OData protocol includes many features but the purpose of this material is only to give the reader an introduction on the topic and familiarize him/her with some on the features.

##Querying Data

Besides the basic functionality one of the most significant functionlality is the support for querying data or query options.

OData supports various types of query options. This query options a defined by using parameters.
Some of them are: 

* search
  * This query option restricts the result to a maching expresion. for example:
    Return all the pruducts that match the string "cool beer"
    http://.../odata/Products?$search="ee"
    
 
* filter
  * This query option filters the result based on a boolean condition. for example:
    Return all products with a price less than 10
    http://.../odata/Products?$filter=Price lt 10.00


* count
  *  This query option return the total count of items with the result. for example:
     Return all products and total count
     http://.../odata/Products?$count=true


* orderby
  * This query option orders the result on the given criteria. for example:
    Return all products ordered by category in ascending order
    http://../odata/Products?$orderby=Category asc
    

* skip
  * This query option skips the first n results. for example:
    Skip the first 5 products
    http://../odata/Products?$skip=5
  
* top
  * This query option returns only the first n the results. for example:
    Return the top 3 product
    http://../odata/Products?$top=3


* expand
  * This query option expands related entities inline. for example: 
    If you have both products and categories and you wish to expand the products for each category
    http://../odata/Categories?$expand=Products


* select
  * This query option selects the properties in the result. for example:
    Return all products price only
    http://../odata/Products?$select=Price 

* format
 * This query option specifies the format of the result. for example:
   Return all products result as json
   http://../odata/Products?$format=json


##OData and .NET

Web API currently support the creation of a OData service. It is important to note that some of the features that the OData protocol specifies are not supported by the framework.

For more information check this tutotial from asp.net on how to [Create an OData v4 Endpoint Using ASP.NET Web API 2.2](http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint)

***[More on OData from asp.net](http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api)***

***Related material***
* [OData](http://www.odata.org/)
* [Odata .NET](http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api)


TODO
http://beyondtheduck.com/projecting-and-the-odata-expand-query-option-possible-at-last-kinda/
