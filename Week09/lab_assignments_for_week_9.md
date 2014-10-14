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




## Exercise 2

Lets write a simple Movie page where users can do the following

- See list of movies
- See movies by filtering on year

This should be a simple service written in Flask. You can have it as web service or as web page. You can decide.

The only design criteria is that the movies are stored in database using SQLAlchemy. The following section is a light tutorial on SQLAlchemy that you might find helpful when solving this exercise.


### SQLAlchemy
SQLAlchemy is a SQL toolkit and Object Relational mapper (ORM). With SQLAlchemy you can define class models that are mapped to database tables and through them you can do SQL queries.

We know go through a simple example on how we can use SQLAlchemy in Python code to store informations on movies in a database.

To use SQLAlchemy in code you must install it first. To install SQLAlchemy use pip as follows.

	pip install sqlalchemy


Let's start py creating a simple python script with the name db.py with the following content.

	from sqlalchemy import create_engine
	from sqlalchemy.ext.declarative import declarative_base
	from sqlalchemy.orm import sessionmaker
	
	engine = create_engine('sqlite://///tmp/testdb.sqlite', echo=True)
	Base = declarative_base()
	Session = sessionmaker(bind=engine)
	
First we create an `Engine` instance with engine configuration.
The Engine is the starting point for any SQLAlchemy application. It’s “home base” for the actual database and its DBAPI, delivered to the SQLAlchemy application through a connection pool and a Dialect, which describes how to talk to a specific kind of database/DBAPI combination.

In this case we are defining Engine to a sqlite file that lives in /tmp/testdb.sqlite. SQLAlchemy supports multiple engine configurations for different database. You can read more on how to configure it for different databases at [http://docs.sqlalchemy.org/en/rel_0_9/core/engines.html](http://docs.sqlalchemy.org/en/rel_0_9/core/engines.html)

Next we create a declarative base. We use this Base as the Base class for our models. Internally, the declarative base class is a catalog of classes and tables relative to that base.

The third value is a Session factory that is bound to our engine. When we communicate with the database we do that through a session created from this factory. The session watches the state of our objects that we query and acts as a transaction manager.

Now, let's define a model. Create a new file called models.py

	from db import Base
	from sqlalchemy import Column, Integer, String
	
	
	class Movie(Base):
	    __tablename__ = 'movies'
	    id = Column(Integer, primary_key=True)
	    title = Column(String, nullable=False)
	    year = Column(Integer, nullable=False)

There are many other column types that SQLAlchemy provides. You can read about them here [http://docs.sqlalchemy.org/en/rel_0_9/core/types.html](http://docs.sqlalchemy.org/en/rel_0_9/core/types.html)

Before you can start querying the database, it must be created. When models have been defined and registered with the declarative base SQLAlchemy can create the database for you. Simply open up your shell (with your virtualenv enabled), run python and do the following

	from db import engine, Base
	from model import Movie
	
	Base.metadata.create_all(engine)
	
This will create the database using the engine defined in db.py. If you are using the sqlite configuration you should now have a sqlite file on your machine.

#### Create movies and save them to database

	from models import Movie	
	from db import Session

	session = Session()
	m = Movie(title='Terminator', year=1995)
	session.add(m)
	session.commit()

#### Count movies

	from models import Movie	
	from db import Session

	session = Session()
	session.query(Movie).count()
	
#### Fetch all movies

	from models import Movie
	from db import Session
	
	session = Session()
	movies = session.query(Movie).all()
	
#### Filter movies by a given field

	from models import Movie
	from db import Session
	
	session = Session()
	movies = session.query(Movie).filter(Movie.year == 2004).all()



#### Read more about the power of SQLAlchemy
- [http://docs.sqlalchemy.org/en/rel_0_9/orm/tutorial.html](http://docs.sqlalchemy.org/en/rel_0_9/orm/tutorial.html)

 
