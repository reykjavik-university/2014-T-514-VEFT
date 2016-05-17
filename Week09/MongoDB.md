MongoDB

NoSQL databases have received a lot of attention the past few years due to an exponential growth in storing and accessing data. One of the most used and popular NoSQL servers MongoDB is especially popular with web programmers. But why is that ?  Lets list some of the more appealing MongoDB features.

MongoDB is a [document](datamodels) driven database or as some call it a “document store” with document stores you are not confined by data schemas, i.e. you can put different values in each document stored in the same collection. The main benefit of this structure is that you can store the data in the database almost exactly as it is represented on the media device web page\app..

MongoDB inserts\reads can on some areas perform substantially faster than relational databases.

Linear scalability its easy to add more servers to your server farm just by adding a new “[shard]”(#shards) to your database and MongoDB takes care of setting up and integrating the shard to the farm.

How ever MongoDB is not the *silver bullet* of databases. Lets take a look at MongoDB cons.

With the schema freedom provided by MongoDB it is more in the hand of the programmer to insure  data integrity.

You could have a lot of recurring data. i.e. when you don’t have relationships you tend to just store the same data over and over again.

Tho MongoDB states that it is perfectly safe to store sensitive data “bank statements and such” i would highly recommend against it.

Additional reading material regarding MongoDB haters 
[Sarah Mei](http://www.sarahmei.com/blog/2013/11/11/why-you-should-never-use-mongodb/)


Data models.
As stated previously data models in MongoDB have flexible schemas called Collections which are similar to tables in SQL relational databases. Collections stores our “collection” of documents in a binary json string called BISON.

Example of a document.
{
	name: ‘Sjurt Lobain’,
	age: 26,
	groups: [ ‘27Club’, ‘Svetlana’, ‘KidsDontDoDrugs’ ]
}
and in the same collection this would be perfectly acceptable but would probably not be an good idea for the database integrity.
{
	name: B1337BS,
	song: ’What’s my age again’,
	trivia: ’All these small things late night work sucks.. and so on’
}




Indexes
Indexes are special data structures that store a small portion of the collection’s data set in an easy to traverse form. The index stores the value of a specific field or set of fields, ordered by the value of the field.

Fundamentally, indexes in MongoDB are similar to indexes in other database systems. MongoDB defines indexes at the collection level and supports indexes on any field or sub-field of the documents in a MongoDB collection.

All MongoDB collections have an index on the _id field that exists by default. If applications do not specify a value for _id the driver or the MongoDB will create an _id field with an ObjectId value.

The _id index is unique, and prevents clients from inserting two documents with the same value for the _id field.

Replication
Replication is a way to increase data availability and provides a redundancy plan for server failure. In a MongoDB cluster the primary node accepts all write operations from clients and replicates it to the secondary nodes. In a replication set you can only have one primary node.

Shards


Installation and setup.

Linux.
Ubuntu

Mac Osx

Windows.

MongoDB Fundamental opperations 
