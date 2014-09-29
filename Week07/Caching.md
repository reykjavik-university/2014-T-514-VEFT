
#Caching

Speed! It is becoming more and more important to serve content to client as fast as possible. One method to aid this is caching. Caching does this by temporarly storing data so that future request to that same data can be served faster.

##Web API caching

In this section we will look a different ways of caching data on Web API services. The are some libraries available for implementing client og server caching. Now a days the most common libraries are: 

* [CacheCow](https://github.com/aliostad/CacheCow)
* [ASP.NET Web API CacheOutput](https://github.com/filipw/AspNetWebApi-OutputCache)

Of course you could also implement your own caching method.


###ETags

Before we look at these two libraries it is important to understand how ETags work.

Etag is basically a unique identifier for web caching validation. It is a unique key generated at the server. 
This key represents a resource(URL), if the resource changes then a new Etag is issued for that resource

Lets say for example we issue a get request (http://.../api/courses/allpersons) to a web service, and lets assume that we have not made a request before. We will get a response with includes the resulting data as well as an Etag. Now if we issue the same request, by including the "If-None-Match" header with the value of the Etag, the server will then compare the header(Etag) with the resource requested and if they match, the cached content will be returned together with a 304 HTTP response(Not modified).

On the other hand if we wanted to issue a PUT/PATCH request we would have to include the "If-Match" header, this will result in the server returning a 412 HTTP response(Precondition Failed)) to the client if the Etag do not match, meaning that the data request has changed.


###CacheCow

CacheCow library implements server and client by means of [message handlers](http://www.asp.net/web-api/overview/advanced/http-message-handlers).

First you need to install [this library](https://www.nuget.org/packages/CacheCow.Server/)

and to start using it you have to add the following code the WebApiConfig class

```c#
...
public static void Register(HttpConfiguration config)
{
	//Caching with CacheCow
	var cacheCowCacheHandler = new CachingHandler(config);
        config.MessageHandlers.Add(cacheCowCacheHandler);
}
...
```

Now as an example if we issue the get request mentioned earlier we will get this:

***Status Code:200 OK***

***Request Header***
* Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
* Accept-Encoding:gzip,deflate,sdch
* Accept-Language:en-US,en;q=0.8,is;q=0.6
* Cache-Control:no-cache
* Connection:keep-alive
* Cookie:...
* Host:localhost:12298
* Pragma:no-cache
* User-Agent:Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.124 Safari/537.36

***Response Headers***
* Cache-Control:no-transform, must-revalidate, max-age=0, private
* Content-Length:10952
* Content-Type:application/xml; charset=utf-8
* Date:Sun, 28 Sep 2014 23:32:09 GMT
* **ETag:W/"c701cee4b1d64c658cc13c7f891139cd"**
* **Last-Modified:Sun, 28 Sep 2014 23:30:40 GMT**
* Server:Microsoft-IIS/8.0
* X-AspNet-Version:4.0.30319
* X-Powered-By:ASP.NET
* X-SourceFiles:...

But the second request:

***Status Code:304 Not Modified***

***Request Headers***
* Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
* Accept-Encoding:gzip,deflate,sdch
* Accept-Language:en-US,en;q=0.8,is;q=0.6
* Cache-Control:max-age=0
* Connection:keep-alive
* Cookie:...
* Host:localhost:12298
* If-Modified-Since:Sun, 28 Sep 2014 23:30:40 GMT
* **If-None-Match:W/"c701cee4b1d64c658cc13c7f891139cd"**
* User-Agent:Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.124 Safari/537.36

***Response Headers***
* Cache-Control:no-cache
* Date:Sun, 28 Sep 2014 23:30:44 GMT
* **ETag:W/"c701cee4b1d64c658cc13c7f891139cd"**
* Expires:-1
* Pragma:no-cache
* Server:Microsoft-IIS/8.0
* X-AspNet-Version:4.0.30319
* X-Powered-By:ASP.NET
* X-SourceFiles:...


Here we notice that that the first request returned a status code 200 with an Etag, then when we issued a second request with the "If-None-Match" header with the Etag value and we got a status code 304, thus cached contents where issued.

As mentioned on there [CacheCow wiki](https://github.com/aliostad/CacheCow/wiki), some features include:

**CacheCow.Server features**

* Managing ETag, Last Modified, Expires and other cache related headers
* Implementing returning Not-Modified 304 and precondition failed 412 responses for conditional calls
* Invalidating cache in case of PUT, POST, PATCH and DELETE
* Flexible resource organisation. Rules can be defined so invalidation of a resource can invalidate linked resources

**CacheCow.Client features**

* Caching GET responses according to their caching headers
* Verifying cached items for their staleness
* Validating cached items if must-revalidate parameter of Cache-Control header is set to true. It will use ETag or Expires whichever exists
* Making conditional PUT for resources that are cached based on their ETag or expires header, whichever exists


###ASP.NET Web API CacheOutput

This library is a litle bit different from CacheCow, it uses attributes to control caching on actions.

To use it install [this library](https://www.nuget.org/packages/Strathweb.CacheOutput.WebApi2/)

As mentioned on [there documentation](https://github.com/filipw/AspNetWebApi-OutputCache), some of the properties you can specifie are:

* *ClientTimeSpan* (corresponds to CacheControl MaxAge HTTP header)
* *MustRevalidate* (corresponds to MustRevalidate HTTP header - indicates whether the origin server requires revalidation of a cache entry on any subsequent use when the cache entry becomes stale)
* *ExcludeQueryStringFromCacheKey* (do not vary cache by querystring values)
* *ServerTimeSpan* (time how long the response should be cached on the server side)
* *AnonymousOnly* (cache enabled only for requests when Thread.CurrentPrincipal is not set)

Lets say that we would like to cache the following action for 30 seconds on both client and server the code would look like this :

```c#
[CacheOutput(ClientTimeSpan = 30, ServerTimeSpan = 30)]
[Route("allpersons")]
public List<Person> GetAllPersons()
{
	return _service.GetPersons();
}
```

CacheOutput offer several options for configuring and appliying cache to your API service. 
Some of these include the possibility to implement you own custom server side cache mechanism. You can also invalidate cache through attributes at controller or action level as well manually. 

For example decorating a controller with [AutoInvalidateCacheOutput] attribute will automatically flush all cached GET data from this controller after a successfull POST/PUT/DELETE request.


*** Related material***
* [AspNetWebApi-OutputCache]()
* [CacheCow](https://github.com/aliostad/CacheCow)
* [Building ASP.Net Web API RESTful Service](http://bitoftech.net/2014/02/08/asp-net-web-api-resource-caching-etag-cachecow/)
* [Exploring Web API 2 Caching](http://damienbod.wordpress.com/2014/05/18/exploring-web-api-2-caching/)










