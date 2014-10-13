# Lab assignment for week 9
This document contains a lab assignment for week 9 in the course 2014-T-514-VEFT.


## Exercise 1
There is a theory that the cosmetic balance of the universe is controlled by the correct amount of parenthesis in our universe.

Don't you just hate it when you sit in a mathematics or a programming class and you spot a formulae or a statement on the whiteboard where there is a missing parenthesis and you can't do anything about it.

No worries. In this exercise we will implement a parenthesis balance API. The aim of this API is as follows. If you spot a missing parenthesis somewhere in the universe you can simply do a curl to your API to inject a new parenthesis to the universe (to fix the balance of course) and you can keep track of number of parenthesis that have been added.

Your api should have one resource `/balance/<direction>` where <direction> can be either left or right. You can do either a `GET` or `POST` to the API and it should operate as follows

- GET /balance/left: returns number of left parenthesis
- GET /balance/right: returns number of right parenthesis
- POST /balance/right (no body) increments the number of right parenthesis
- POST /balance/left (no body) increments the number of left parenthesis

You can also add a route on `/` that shows the number for both the values left or right that you can then expose to the world.

The following requirements for this exercise are

1. It should be written in Python using [Flask](http://flask.pocoo.org/)
2. The values should be stored in [Memcached](http://memcached.org/)
3. You can use vanilla Flask or you can use [Flask-restful](http://flask-restful.readthedocs.org/en/latest/)

The curl commands that you should be using while developing your server might look something like as follows.

	➜  ~  curl http://localhost:5000/balance/right
	{
	  "right": 3
	}%
	
	➜  ~  curl -X post http://localhost:5000/balance/right
	ok%

	➜  ~  curl http://localhost:5000/balance/right
	{
	  "right": 4
	}%




## Crash course for Memcached
Memcached is free and open source, high-performance, distributed memory object caching system. Or, it is just simply a key value pair store service. You can think of it as a distributed hash table that you can use to store key value pair data. It is an external service that you can communicate with over a network.

To install Memcached on OSX

	brew install memcached

or on Ubuntu

	sudo apt-get install memcached
	
You can then start a Memcached deamon with

	memcached --v
	
In a separated terminal. Keep it open while you are working on this exercise.

To communicate with with Memcached from Python we use a Python client named python-memcached

	pip install python-memcached
	
In code you can connect to the memcached server as follows.

	memcached_host = '127.0.0.1:11211'
	client = memcache.Client([memcached_host], debug=True)
	
	# Set value
	client.set('left', 0)
	
	# get value (returns None if key has not been set)
	value = client.get('left')

	# you can increment a value that has been set
	client.incr('right')

You can read the documentation for python-memcached at [https://github.com/linsomniac/python-memcached/blob/master/memcache.py](https://github.com/linsomniac/python-memcached/blob/master/memcache.py)
