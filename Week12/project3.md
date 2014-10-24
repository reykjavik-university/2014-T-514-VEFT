# Project 3 - Kodemon

This document describes the third and final assignment in the course
T-514-VEFT, Vefþjónustur, 2014-3 at Reykjavík University, fall 2014.

In this assignment we implement a code monitoring system that we call Kodemon.

The idea of Kodemon is as follows. You place a decorator on Python functions as this code demonstrates.

    from kodemon import kodemon

    @kodemon
    def fetch_latest_entries():
    	...
    	return entries

When this function is executed, behind the scenes, the decorator sends a [UDP](http://en.wikipedia.org/wiki/User_Datagram_Protocol) package to a UDP server with information on the function execution, such as the time it took to run the function.

The server parses and stores the content of the message in some data storage and exposes them in an API.

Each function have their own key and over a time kodemon collects information on how a given function have been performing over a period of time. With this in hand you can monitor parts of application that you write even after they have been deployed to a live server.

## Kodemon Python decorator
You don't have to implement the Kodemon Python decorator. It is given and can be viewed on [https://github.com/hlysig/kodemon-python](https://github.com/hlysig/kodemon-python)

You can install it into your project virtualenv with

	pip install git+https://github.com/hlysig/kodemon-python

Note: Make sure that the path to your working directory does **not** contain any spaces as this command may fail if that's the case (e.g. `/home/smu/project3`, not `/home/smu/project 3`).

By default, if placed on a function that is executed, it will send a UDP message to localhost on port 4000. The message that the decorator sends is a Json a string on the form

	{"execution_time": 0.031948089599609375, "timestamp": 1413404125, "token": "test-token", "key": "foo.py-foobar"}


	
By default these are the values that are in the message.

- `key`: Combined from the name of the python file, where the function resides, and the function name. 
- `execution_time` The time it took to execute the function.
- `timestamp` [Epoch](http://www.epochconverter.com/) timestamp when the function was executed
- `token` Can be used to identify whom is sending the message. The token can represent a project, system or a customer. By default this value is set to "test-token".

To configure which host and port the decorator sends to you can export the following [environment variables](http://en.wikipedia.org/wiki/Environment_variable) into your shell before you execute your Python application.

	export KODEMON_HOST="some.host.com"
	export KODEMON_PORT="1337"
	export KODEMON_TOKEN="customer-token"
	
If you don't the decorator will always send to localhost:4000.
	
You can also add new, or override values in the message by adding parameters to the kodemon decorator as follows.

	@kodemon(key1=value1, .. keyn=valuen)
	
But you can always expect to find the above mentioned values (key, value, timestamp, token) in the message from the decorator.
	
## Kodemon UDP server (40%)
The first task in this project is to write a UDP server which receives and parses the messages from the Kodemon decorator. You can write the server in Python or NodeJS. The purpose of this server is to, as mentioned, parse the messages and store them in some central data store, such as in a database or in a search index.

There are two possible implementation that you should follow.

1. Store the messages database
	- MongoDB if you are using NodeJS
	- SQLite if you are using Python

2. Store the data in ElasticSearch.
3. Or, store them in both database and ElasticSearch.

To get you going, here are two simple implementations of UDP servers in both Python and NodeJS.

The following code is a UDP server, written in JavaScript/NodeJS, that prints out a message sent from Kodemon.

	var dgram = require("dgram");
	
	var server = dgram.createSocket("udp4");
	
	server.on("message", function(msg, rinfo){
	  console.log('got message from client: ' + msg);
	});
	
	server.on('listening', function(){
	  console.log('Kodemon server listening on')
	  console.log('hostname: ' + server.address().address);
	  console.log('port: ' + server.address().port);
	});
	
	server.bind(4000)
	
	
The following code is a UDP server, written in Python, that prints out a message sent from Kodemon.

	import socket
	
	UDPSock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
	
	listen_addr = ("localhost", 4000)
	UDPSock.bind(listen_addr)
	
	
	while True:
	    data, addr = UDPSock.recvfrom(1024)
	    print data.strip(), addr



## Kodemon API (40%)
The next part in this project is to write an API on top of the data collected by the Kodemon UDP server. The API should support the following methods.

1. List all keys (without any values) that have been sent to the server. With the method you would see a list of all the methods that have been sending messages to the server.
- List all execution times for a given key.
- List all execution times, for a given key on a given time range.

If you want to support many customers or projects, you can use the token that is sent with the message to distinguish between them but this is not required in this assignment.



## Interface which presents the data (20%)
Create a web interface to present the data from Kodemon.
The following requirements must implemented.

- As a user I can see a list of all the functions that have been sending data to the server.
- As a user I can select a key and view the execution times for the function.
- As a user I can select a time range and view only the execution times on that range.

This application must use the Kodemon API that you implement in the previous section.

And yeah. An extra cool feature is to show a self updatable graph such as this one here [http://www.highcharts.com/demo/dynamic-update](http://www.highcharts.com/demo/dynamic-update).

For this part you can use either Flask or Express.

HAPPY HAXXING!


## Project groups
You can work in groups no larger the three max!

## Evaluation
TBA.



