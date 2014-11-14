#MongoDB

NoSQL databases have received a lot of attention the past few years due to an exponential growth in storing and accessing data. One of the most used and popular NoSQL servers MongoDB is especially popular with web programmers. But why is that ?  Lets list some of the more appealing MongoDB features.

Data structure
> MongoDB is a document driven database or as some call it a “document store” with document stores you are not confined by data schemas, i.e. you can put different values in each document stored in the same collection. The main benefit of this structure is that you can store the data in the database almost exactly as it is represented on the media device "web page\app.."

Performance
> MongoDB inserts\reads can on some areas perform substantially faster than relational databases.

Scalability
> Linear scalability its easy to add more servers to your server farm just by adding a new shard to your database and MongoDB takes care of setting up and integrating the shard to the farm.

How ever MongoDB is not the *silver bullet* of databases. Lets take a look at MongoDB cons.

Data integrity
> With the schema freedom provided by MongoDB it is more in the hand of the programmer to insure  data integrity.

> Tho MongoDB states that it is perfectly safe to store sensitive data “bank statements and such” I would highly recommend against it and leave that task to the schema\relational databases.

Bloated database
> You could have a lot of recurring data. i.e. when you don’t have relationships you tend to just store the same data over and over again.

Additional reading material regarding MongoDB haters 
[Sarah Mei](http://www.sarahmei.com/blog/2013/11/11/why-you-should-never-use-mongodb/)


##Data models.
As stated previously data models in MongoDB have flexible schemas called Collections which are similar to tables in SQL relational databases. Collections stores our “collection” of documents in a binary json string called BISON.

**Example of a document.
```
{
	name: ‘Sjurt Lobain’,
	age: 26,
	groups: [ ‘27Club’, ‘Svetlana’, ‘KidsDontDoDrugs’ ]
}
```

and in the same collection this would be perfectly acceptable but would probably not be an good idea for the database integrity.
```
{
	name: B1337BS,
	song: ’What’s my age again’,
	trivia: ’All these small things late night work sucks.. and so on’
}
```

##Indexes
Indexes are special data structures that store a small portion of the collection’s data set in an easy to traverse form. The index stores the value of a specific field or set of fields, ordered by the value of the field.

Fundamentally, indexes in MongoDB are similar to indexes in other database systems. MongoDB defines indexes at the collection level and supports indexes on any field or sub-field of the documents in a MongoDB collection.

All MongoDB collections have an index on the _id field that exists by default. If applications do not specify a value for _id the driver or the MongoDB will create an _id field with an ObjectId value.

The _id index is unique, and prevents clients from inserting two documents with the same value for the _id field.

##Replication
Replication is a way to increase data availability and provides a redundancy plan for server failure. In a MongoDB replica set the primary node accepts all write operations from clients and replicates it to the secondary nodes. For each replica set you can only have one primary node.

Additional reading material about [replication] (http://docs.mongodb.org/manual/core/replication-introduction/)

##Sharding

Sharding is a method for storing data across multiple machines. MongoDB uses sharding to support deployments with very large data sets and high throughput operations. 
To enable a sharding service in MongoDB you need to set up a shardered cluster shard cluster **requires three components.**
 
[Shards](http://docs.mongodb.org/manual/reference/glossary/#term-shard) store the data. To provide high availability and data consistency, in a production sharded cluster, each shard is a replica set

[Query routers](http://docs.mongodb.org/manual/reference/glossary/#term-mongos) or mongos instances, interface with client applications and direct operations to the appropriate shard or shards. The query router processes and targets operations to shards and then returns results to the clients. A sharded cluster can contain more than one query router to divide the client request load. A client sends requests to one query router. Most sharded clusters have many query router

[Config server](http://docs.mongodb.org/manual/reference/glossary/#term-config-server) Store the cluster’s metadata. This data contains a mapping of the cluster’s data set to the shards. The query router uses this metadata to target operations to specific shards. Production sharded clusters have exactly 3 config servers.

Additional reading material about [Sharding] (http://docs.mongodb.org/manual/core/sharding-introduction/)
 

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

# Validating in mongoose

* Validation is defined in the schema
* Validation occurs when document attempts to be saved, after default values have been saved
* Validation is asynchronously recursive, when you call the save function validation is executed. If an error occurs your save function callback receives it.

## Simple validation

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

## Regular expression

You can also validate by using regular expression

Example:
```
var schema = new Schema({
    name: { type: String, validate: /[a-z]/ }
});
```

## Asynchronous validation

You can define a validator function with two parameters like:
```
schema.path('name').validate(function (v, fn) {
  // my logic
}, 'my error type');
```

Then the function fn will be called with true or false depending on whether the validator passed
This allows for calling other models and querying data asynchronously from your validator.


## Built in validators.

* Strings:

*enum: takes a list of allowed strings.*
```
	var Post = new Schema({
    		type: { type: String, enum: ['smuu', 'foo', 'bar'] }
	})
```

* Numbers:

*min: minimum value*
```
	var Person = new Schema({
    		age: { type: Number, min: 10 }
	})
```	

*max: maxmimum value*
```
	var Person = new Schema({
    		age: { type: Number, max: 42 }
	})
```
