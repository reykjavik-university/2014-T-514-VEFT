MongoDB

NoSQL databases have received a lot of attention the past few years due to an exponential growth in storing and accessing data. One of the most used and popular NoSQL servers MongoDB is especially popular with web programmers. But why is that ?  Lets list some of the more appealing MongoDB features.

MongoDB is a document driven database or as some call it a “document store” with document stores you are not confined by data schemas, i.e. you can put any value in any column.
A column can have one ore more value or even a nested structure.

Ex:
{
	name: ‘Sjurt Lobain’,
	age: 26,
	groups: [ ‘27Club’, ‘Svetlana’, ‘KidsDontDoDrugs’ ]
}
and in the same document this would be perfectly acceptable but would probably not be an good idea for a database integrity.
{
	name: 1337,
	age: ’What’s my age again’,
	groups: ’All these small things late night work sucks.. and so on’
}
 
With a document database you can store your data almost as it is represented on your website \ program. 

MongoDB inserts\reads can on some areas perform substantially faster than relational databases.

Linear scalability its easy to add more servers to your server farm just by adding a new “[shard]”(#shards) to your database and MongoDB takes care of setting up and integrating the shard to the farm.

How ever MongoDB is not the *silver bullet* of databases. Lets take a look at MongoDB cons.

With such schema freedom if handled wrong you could completely destroy your data integrity like storing string values in a column where your program usually expects to get a number reply.

You could have a lot of recurring data. i.e. when you don’t have relationships you tend to just store the same data over and over again.
..
..
.. some more flaws.

Datamodels.

Indexes

Replication

Shards


Installation and setup.

Linux.
Ubuntu

Mac Osx

Windows.

MongoDB Fundamental opperations 
