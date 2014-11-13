#MongoDB

MongoDB is a NoSQL database that uses a document-oriented data model.  MongoDB does not use tables and rows as relational
databases do but instead all data is stored in documents and collections.  MongoDB is therefore schema free.
Data is stored on BSON format which is a binary-encoded serialization of JSON-like documents.  
These objects are added to a collection.  Collections are similar to tables in a relational database.

MongoDB is a fast and scalable database, it is good for many things but it is no recommended to use this as a database for
applications that store sensitive data.

It is easy to run many instances of MongoDB, if that is done the instances replicate the data between them.

MongoDB does not support traditional sql query language.   Instead it offers its own query language and it is easy
to find good information about that on the official MongoDB website.

MongoDB is a database server that have to be installed to be used.
##Setup
Ubuntu:
> sudo apt-get install mongodb

Mac OSX using brew:
> brew install mongodb

Mac OSX alternative:
> curl -O http://downloads.mongodb.org/osx/mongodb-osx-x86_64-2.6.4.tgz

> tar -zxvf mongodb-osx-x86_64-2.6.4.tgz

> mkdir -p mongodb

> cp -R -n mongodb-osx-x86_64-2.6.4/ mongodb

The server is started from the command line with the command:

> sudo service mongod start
This command starts up the mongo daemon.

MongoDB needs a data folder and it has to be created with sudo rights. By default it stores its data in /var/lib and logs in /var/log/mongdb, but you can also create your one like this:

> sudo mkdir -p /data/db

And then behind closed doors let’s give everyone access rights to this folder:

> chmod –R 777 /data/db

#Mongo

Mongo is a console based client that can be used to query data in MongoDB.  There are also other tools available online
that have more visual interface as [Robomongo](http://robomongo.org/)

This command lists all databases
> show database

This command switches or creates the database mydb
> use mydb

This command shows all collections in mydb.
> show collections  

To create a collection, create the JSON object
> var x = {‘user’: ‘hlysig’, ‘course’:’Forritun 1’, ‘grade’:’4’}

To insert into the grades collection give the command:
> db.grades.insert(x)

Further information can be found on [mongoDB](http://www.mongodb.org)

#Mongoose

[Mongoose](http://mongoosejs.com/) is a framework that can be used in applications to connect to MongoDB.
##Setup
> npm install mongoose

##Getting started
To include mongoose in your project
> var mongoose = require('mongoose');

> mongoose.connect('mongodb://localhost/test');

Then to create a new schema you can do something this:

```
var mySchema = new mongoose.Schema({
	name: String,
	birthday: { type: Date, default: Date.now },
	age: Number
});
```
You can even add method to our schema like this. Please note that if we want methods we have to add them before compiling with mongoose.model()
```
mySchema.methods.info = function () {
  var greeting = this.name
    ? "My name is " + this.name
    : "I'm sorry I don't have a name"
}
```
Then to create our mySchema we need to pass it into mongoose.model(modelName, schema) and then call the save method

```
var Person = mongoose.model('Person', mySchema);
```
Now we can create our person
```
var person = new Person({ name: 'Dabs', age: 7 }) // We don't need to set our date, since we have a default
```
And then to save it in MongoDB we call save
```
person.save(function (err, person) {
  if (err) return console.error(err);
  person.info(); //Should say "My name is Dabs"
```
To query for all persons we can do
```
Person.find(function (err, persons) {
  if (err) return console.error(err);
  console.log(persons);
})
```
For more information about queries see [here](http://mongoosejs.com/docs/queries.html)

## Validating in mongoose

Validation is defined in the schema
Validation occurs when document attempts to be saved, after default values have been saved
Validation is asynchronously recursive, when you call the save function validation is executed. If an error occurs your save function callback receives it.

#Simple validation

Simple validation is declared by passing a function to validate and error type to your SchemaType.

Example:
```
function validator (v) {
  return v.length > 10;
};

new Schema({
    name: { type: String, validate: [validator, 'my error type'] }
})
```

Alternatively you can do the same with this:

```
var schema = new Schema({
    name: String
})

schema.path('name').validate(function (v) {
  return v.length > 5;
}, 'my error type');
```

#Regular expression

You can also validate by using regular expression

Example:
```
var schema = new Schema({
    name: { type: String, validate: /[a-z]/ }
});
```

#Asynchronous validation

You can define a validator function with two parameters like:
```
schema.path('name').validate(function (v, fn) {
  // my logic
}, 'my error type');
```

Then the function fn will be called with true or false depending on whether the validator passed
This allows for calling other models and querying data asynchronously from your validator.


Mongoose also has some built in validators.

Strings:
	enum: takes a list of allowed strings.
	```
	var Post = new Schema({
    type: { type: String, enum: ['smuu', 'foo', 'bar'] }
		})
		```

Numbers:
		min: minimum value
		```
		var Person = new Schema({
    age: { type: Number, min: 10 }
		})
		```

		max: maxmimum value
		```
		var Person = new Schema({
    age: { type: Number, max: 42 }
		})
		```
