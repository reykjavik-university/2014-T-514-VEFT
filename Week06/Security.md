
***NOTE: this document is still a work in progress as it summarises some of the the main topics in discussion, it also quotes on references and articles***

# Security

## OAuth

Many luxury cars today come with a valet key. It is a special key you give the parking attendant and unlike your regular key, will not allow the car to drive more than a mile or two. Some valet keys will not open the trunk, while others will block access to your onboard cell phone address book. Regardless of what restrictions the valet key imposes, the idea is very clever. You give someone limited access to your car with a special key, while using your regular key to unlock everything.

Every day new websites launch offering services which tie together functionality from other sites. A photo lab printing your online photos, a social network using your address book to look for friends, and APIs to build your own desktop application version of a popular site. These are all great services – what is not so great about some of the implementations is their request for your username and password to the other site. When you agree to share your secret credentials, not only do you expose your password to someone else (yes, that same password you also use for online banking).

This is the problem OAuth solves. It allows you, the User, to grant access to your private resources on one site, to another site. While OpenID is all about using a single identity to sign into many sites, OAuth is about giving access to your stuff without sharing your identity at all (or its secret parts).

### OAuth 1.0

OAuth Core 1.0 (also known as RFC 5849), the community-based specification published on December 4th, 2007, revised June 24th, 2009, and finalized in April 2010 is one of the fastest growing Open Web specifications. It provides a much needed solution for security web APIs without requiring users to share their usernames and passwords.

In the traditional client-server authentication model, the client uses its credentials to access its resources hosted by the server. OAuth introduces a third role to this model: the resource owner. In the OAuth model, the client (which is not the resource owner, but is acting on its behalf) requests access to resources controlled by the resource owner, but hosted by the server.

In order for the client to access resources, it first has to obtain permission from the resource owner.  This permission is expressed in the form of a token and matching shared-secret.  The purpose of the token is to make it unnecessary for the resource owner to share its credentials with the client.  Unlike the resource owner credentials, tokens can be issued with a restricted scope and limited lifetime, and revoked independently.

### OAuth 2.0

OAuth 2.0 attempted to replace 1.0 with a simpler protocol but ended up creating a loose framework that is very hard to implement securely. The lead author and editor claims that the OAuth 2.0 specification ended up being so poorly done and 
because of this he is [no longer involved](http://hueniverse.com/2012/07/26/oauth-2-0-and-the-road-to-hell/) in the 2.0 effort and has withdrawn his name and support from it. He highly recommend you use OAuth 1.0 as 2.0 is likely to create security exploits.

***[This example explains the protocol workflow](http://hueniverse.com/oauth/guide/workflow/)***

### OAuth and .NET

Building an application in .NET which support OAuth is relatively easy.

Microsoft's [ASP.NET Identity](http://www.asp.net/identity/overview/getting-started/introduction-to-aspnet-identity) system is designed to replace the previous ASP.NET Membership and Simple Membership systems. It includes profile support, OAuth integration, works with [OWIN](http://www.asp.net/aspnet/overview/owin-and-katana), and is included with the ASP.NET templates shipped with Visual Studio 2013.

For example, when building a MVC 5 Application the support for enabling users to log in using OAuth 2.0 with credentials from an external authentication provider, such as Facebook, Twitter or Google is already supported out of the box. [This tutorial explains how](http://www.asp.net/mvc/tutorials/mvc-5/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on)

Futher more, this tutorial exaplains how to create your own [OAuth 2.0 Authorization Server using OWIN OAuth middleware](http://www.asp.net/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server)

***Links***
* [The OAuth 1.0 Protocol](http://tools.ietf.org/html/rfc5849#section-1.2)
* [OAuth](http://oauth.net/)
* [ASP.NET Identity](http://www.asp.net/identity/overview/getting-started/introduction-to-aspnet-identity)


##OpenID Connect

OpenID Connect 1.0 is a simple identity layer on top of the OAuth 2.0 protocol. It enables Clients to verify the identity of the End-User based on the authentication performed by an Authorization Server, as well as to obtain basic profile information about the End-User in an interoperable and REST-like manner.

OpenID Connect is an interoperable authentication protocol based on the OAuth 2.0 family of specifications. It uses straightforward REST/JSON message flows with a design goal of “making simple things simple and complicated things possible”. It's uniquely easy for developers to integrate, compared to any preceding Identity protocol.

OpenID Connect lets developers authenticate their users across websites and apps without having to own and manage password files. For the app builder, it provides a secure verifiable, answer to the question: “What is the identity of the person currently using the browser or native app that is connected to me?”

OpenID Connect allows for clients of all types, including browser-based JavaScript and native mobile apps, to launch sign-in flows and receive verifiable assertions about the identity of signed-in users.

(Identity, Authentication) + OAuth 2.0 = OpenID Connect

***What OpenID Connect does is basically add authentication on top of OAuth.***

One could say that the main difference between these protocols is that OAuth defined access tokens, these tokens are then used to access providers, for example get some information from facebook/google. On the other hand OpenID Connect defined identity tokens which are used to identify the user, for example to access an application.

The OpenID Connect protocol, in abstract, follows the following steps.

1. The Client sends a request to the OpenID Provider.
2. The OpenID Provider authenticates the End-User and obtains authorization.
3. The OpenID Provider responds with an ID Token and usually an Access Token.
4. The Client can send a request with the Access Token to the UserInfo Endpoint.
5. The UserInfo Endpoint returns Claims about the End-User.

[This video explains the basics of the protocol](https://www.youtube.com/watch?feature=player_embedded&v=Kb56GzQ2pSk)

Also, [this video](http://vimeo.com/97344501), which is a talk by Dominick Baier held at the 2014 NDC explains the differences between OpenID Connect & OAuth in an interest way. ***You are encouraged to watch it!***

OAuth 2.0, is a framework, specified by the IETF in RFCs 6749 and 6750 (published in 2012) designed to support the development of authentication and authorization protocols. It provides a variety of standardized message flows based on JSON and HTTP, OpenID Connect uses these to provide Identity services.

There are several [libraries](http://openid.net/developers/libraries/) available in different languages that implement OpenID Connect and related specifications.

One of these libraries is the [IdentityServer v3](https://github.com/thinktecture/Thinktecture.IdentityServer.v3) which is demonstrated in the [talk by Dominick Baier](http://vimeo.com/97344501)

IdentityServer v3 is a .NET-based open source implementation of an OpenID Connect and OAuth2 security token service.

###OpenID Connect basic specs

####Flows

The OpenID Connect authentication process consists by requiring the user to authenticate him/her self through a OpenID provider. This process then returns the information about the user in the form of an id token, which contains scopes and claims, as well as other useful information about the user being authenticated.

The way in which this process is conducted is referred as a flow. The main flows are:

* Code Flow
* [Implicit Flow](http://stackoverflow.com/questions/13387698/why-is-there-an-authorization-code-flow-in-oauth2-when-implicit-flow-works-s)
* Hybrid Flow

This flow can be specified throught the 'response_type' parameters in the authorization request.

#####Code Flow

The basic steps of this flow are the following:

1. The client sends an authentication request to the OpenID provider.
2. The OpenID provider authenticates the user (on behalf of the client) and responds with an authorization code to the client.
3. The client requests an id token with the authorization code.
3. The client validates the token and retrives the user information.

##### Implicit Flow

The basic steps of this flow are the following:

1. The client sends an authentication request to the OpenID provider.
2. The OpenID provider authenticates the user (on behalf of the client) and redirects him/her to the client with an id token.
3. The client validates the token and retrieves the user information.

#####Hybrid Flow

This flow is basically a mix of the other two, which means that in some cases the authorization process is return to the client with a code and sometimes with an id token directly.


####Request Parameters

Some of the request parameters or main ones are: 

* client_id       : This is the client identifier.
* response_type   : this is the reponse type. Like 'id-token' or token.
* scope           : This represents the access or information request. F.e. openid, profile, email, read or write.
* redirect_uri    : This is the location for where the response will be sent.
* response_mode   : This defines the Form Post Response Mode.
* state           : This is for maintaining a state between the client and provider.
* nonce           : This is used to associate a client session with an id token.

#### ID token

As we talked about earlier the id token contains information about the authenticated user in the form of a claim. Id tokens as represented in a [JSON Web Token](http://jwt.io/) format. 
Some of the claims contained within an id token are the following:

* iss      : This represents the issuer of the response (url).
* sub      : This is the subject identifier, a unique identifier that represents a user.
* aud      : This is the audience which is a client identifier.
* exp      : This represents the expiration date of the id token.
* iat      : This represents the issue date.
* nonce    : Again, this is used to associate a client session with an id token.
* at_hash  : This represents the hashed access token.

####Scopes

OpenID Connect defines the following scope values:

* openid         : This represents the authorization request
* profile        : This represents a profile claim request
* email          : This represents an email claim request
* address        : This represents an address claim request
* phone          : This represents a phone claim request
* offline_access : This represents an OAuth 2.0 Refresh Token request

It is of course also possible to define custom scopes in order to achieve some functionality.

####Claims

The OpenID Connect also defines several sets of claims.
For more information on Claim [please refer to these specs](http://openid.net/specs/openid-connect-basic-1_0.html#StandardClaims).


####Comparison between OAuth and OpenID

This wiki link compares OAuth and OpenID and also has a simple picture which explains the differences in the processes.
[OpenID vs pseudo-authentication using OAuth] (http://en.wikipedia.org/wiki/OAuth#OpenID_vs._pseudo-authentication_using_OAuth)


***Links***
* [OpenID Connect](http://openid.net/)
* [Core OpenID Connect functionality specification ](http://openid.net/specs/openid-connect-core-1_0.html)
* [Thinktecture IdentityServer v3](https://github.com/thinktecture/Thinktecture.IdentityServer.v3)
* [Thinktecture IdentityServer v3 Samples](https://github.com/thinktecture/Thinktecture.IdentityServer.v3.Samples)
* [What is the difference between OpenID and OAuth?](https://stackoverflow.com/questions/1087031/whats-the-difference-between-openid-and-oauth)

