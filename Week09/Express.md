# Express
[Express.js](http://expressjs.com/) is lightweight, minimalist web framework for Nodejs.

Express is the most popular web framework that has been written for Node because of its robust set of features and support for many kinds of applications. 

Using a framework like Express for developing web applications has many advantages, such as:  
+ Less time needed to create web applications
+ Routing and view layers are handled by the framework
+ Stable code that is regularly used, tested and maintained.
+ Assumptions made about developers needs so that less code needs to be written - don't reinvent the wheel

Express isn't the be-all and end-all for Node frameworks many exciting frameworks are being developed like [Sails.js](http://sailsjs.org) and [Total.js](https://www.totaljs.com/).

In this example we are going to focus on using Express as a RESTful server which is also its most popular use case.

## Download and install
There are quite a few ways to set up an Express application on your machine.
You can use generators like [Yeoman](http://yeoman.io/) or use Express own generator which can be installed using:

    npm install express-generator -g

and run with:

    express myapp

But our example is going to be simple so we are just going to start from scratch.
When we start working on a web application then a lot of time goes into creating boilerplate code, creating folder structures and downloading dependencies. Generators handle this for us and more.

## Middleware
As was stated before Express is a lightweight framework which only encapsulates the most important parts of an HTTP server, so if we need more features middleware is here to save the day.
Some of the more popular middleware that we can use to make our life easier include:

+ `body-parser` parses the body of HTTP requests to JSON format.
+ `fs` allows you to use the file system of the server.
+ `mongoose` abstraction for talking to MongoDB.
+ `passport` authentication service for Node.js


## Lets dance
So now we know a little bit about express lets write the classic TODO app in a modular way using RESTful principles.
Lets start with the file structure, this is a very basic application so it will have basic file structure.

    TODOapp
    |
    +--server.js
    |
    +--routes.js
    |
    +--models
    |  |
    |  +--contains our database models
    |
    +--controllers
       |
       +--contains our CRUD controllers

The npm packages used in the example below are:
```
npm install express
npm install path
npm install mongoose
npm install fs
npm install body-parser
```

Alright lets first define our TODO schema under the models folder, which will use `mongoose` to talk to MongoDB.

```javascript
var mongoose = require('mongoose'),
	Schema = mongoose.Schema;

var TodoSchema = new Schema({
	title: String,
	dueDate: Date,
	priority: Number
});

mongoose.model('Todo', TodoSchema);
```

A fairly simple Schema that uses three different data types, you can check out other datatypes that mongoose offers [here](http://mongoosejs.com/docs/schematypes.html).

So we got our Schema defined now we need to add some operations to our Schema. Because this is a RESTful API we need to define CRUD methods. We add these methods in a separate file under the controllers folder.

```javascript
var mongoose = require('mongoose'),
	Todo = mongoose.model('Todo');

exports.todo = function(req, res, next, id) {
	Todo.findById(id, function(err, todo) {
		if (err) return next(err);
		if (!todo) return next(new Error('Failed to load TODO ' + id));
		req.todo = todo;
		next();
	});
};

exports.show = function(req, res) {
	res.json(req.todo);
};

exports.query = function(req, res) {
	Todo.find().exec(function (err, todo) {
		if (err) return res.json(500, err);
		res.json(todo);
	});
};

exports.create = function(req, res) {
	var todo = new Todo(req.body);
	todo.save(function (err) {
		if (err) return res.json(500, err);
		res.json(todo);
	});
};

exports.update = function(req, res) {
	Todo.update({_id: req.todo._id}, req.body, { }, function (err, newTodo) {
		if (err) return res.json(500, err);
		res.json(newTodo);
	});
};

exports.remove = function(req, res) {
	var todo = req.todo;

	todo.remove(function (err) {
		if (err) return res.json(500, err);
		res.json(todo);
	});
};
```

Here we have defined CRUD operations on our TODO schema and if you need a refresher on the `mongoose` syntax, check out [this](http://mongoosejs.com/docs/index.html) guide.

Now we need to map our newly created functions to routes, so lets checkout our routes.js file in the project root directory.

```javascript
var todos = require('./controllers/todos');

module.exports = function(app) {

	// Finds a todo based on its id
	app.param('todoId', todos.todo);

	// CRUD operations
	app.post('/api/todos', todos.create);
	app.get('/api/todos', todos.query);
	app.get('/api/todos/:todoId', todos.show);
	app.put('/api/todos/:todoId', todos.update);
	app.delete('/api/todo/:todoId', todos.remove);
};
```

This will route our CRUD operations to given routes. One thing to note here is the app.param method, this works as middleware to the request and actually fetches the record based on the todoId and relays that record to the actual functions, pretty DRY ehh?
Now we have a basic API end point for our TODO schema, what we have to do now is to bring it all together in our server.js file.

```javascript
var express = require('express'),
	path = require('path'),
	fs = require('fs'),
	bodyParser = require('body-parser'),
	mongoose = require('mongoose');

// Set up our connection to MongoDB using mongoose
var db = mongoose.connect('mongodb://localhost/express-test');

// Get our models using fs
var modelsPath = path.join(__dirname, 'models');
fs.readdirSync(modelsPath).forEach(function (file) {
	if (/(.*)\.(js$)/.test(file)) {
		require(modelsPath + '/' + file);
	}
});

// Set up express
var PORT = 3000;
var app = express();

// only accept json body
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

require('./routes')(app);

// Start the server
app.listen(PORT, function () {
	console.log('Express server listening on localhost:%d', PORT);
});
```

Now we have everything, lets test it out. Start by running MongoDB then run node server.js

Test our server with Curl, POST:
```
curl -i -H "Content-Type: application/json" -X POST -d '{"title": "Get to the chopper", "dueDate": "2014-11-09T14:23:31+00:00", "priority": 1}' http://127.0.0.1:3000/api/todos
```

GET:
```
curl -i http://127.0.0.1:3000/api/todos
```

See if we can find by id:
```
curl -i http://127.0.0.1:3000/api/todos/{id from first curl}
```

Awesome, our server is up and running and works. The good thing about this design is that our server can easily expand to more endpoints and be easily configured.

## MEAN Stack

If you want to develop with Express and like JavaScript the [MEAN](http://mean.io/) stack has been gaining a big following. 
MEAN stands for MongoDB, Express.js, Angular.js, Node.js and is a fullstack solution that comes preconfigured with many helpful packages like Mongoose and Passport. Creating web applications that are fast, robust and maintainable is the main goal of the MEAN stack.

## Helpful resources

+ [Express](http://expressjs.com/)
+ [Passport](http://passportjs.org/)
+ [MEAN](http://learn.mean.io/)
+ [Yeoman](http://yeoman.io/)
+ [Mongoose docs](http://mongoosejs.com/docs/index.html)
+ [body-parser guide](https://github.com/expressjs/body-parser)
+ [Curl guide](http://blog.scottlowe.org/2014/02/19/using-curl-to-interact-with-a-restful-api/)
+ [Mongodb docs](http://docs.mongodb.org/manual/)